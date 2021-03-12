using LunchOrderManagement.DbContexts;
using LunchOrderManagement.Entities;
using LunchOrderManagement.Models.Pagination;
using LunchOrderManagement.Models.Role;
using LunchOrderManagement.Models.User;
using LunchOrderManagement.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Controllers
{
    [Authorize(Roles = "System Administrator")]
    public class UserController : Controller
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly RoleManager<AppIdentityRole> _roleManager;
        private readonly LunchOrderDbContext _context;

        public UserController(UserManager<AppIdentityUser> userManager,
                                RoleManager<AppIdentityRole> roleManager,
                                LunchOrderDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Manage(int page, int pageSize, string keyword)
        {
            List<UserViewModel> users;
            if (String.IsNullOrEmpty(keyword))
            {
                users = _userManager.Users.Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    Email = u.Email,
                    FullName = $"{u.FirstName} {u.LastName}",
                }).ToList();
            }
            else
            {
                keyword = keyword.ToLower();
                users = _userManager.Users.Where(u => 
                                    u.FirstName.ToLower().Contains(keyword) || u.LastName.ToLower().Contains(keyword))
                                    .Select(u => 
                                            new UserViewModel()
                                            {
                                                Id = u.Id,
                                                Email = u.Email,
                                                FullName = $"{u.FirstName} {u.LastName}",
                                            }).ToList();
            }
            if (users == null)
            {
                users = new List<UserViewModel>();
            }
            foreach (UserViewModel u in users)
            {
                u.RolesName = await GetRoles(u.Id);
            }
            if (page == 0) page = 1;
            if (pageSize == 0) pageSize = 4;
            Pager pager = new Pager(totalItems: users.Count, currentPage: page, pageSize: pageSize);
            PaginationViewModel<UserViewModel> model = new PaginationViewModel<UserViewModel>()
            {
                Pager = pager,
                Items = users.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize)
            };

            model.PageSizeItem = new int[] { 4, 8, 16 };
            ViewBag.pageSize = pageSize;
            ViewBag.keyword = keyword;
            return View(model);
        }

        private async Task<IEnumerable<string>> GetRoles(string userId)
        {
            IEnumerable<string> listRoles = new List<string>();
            var user = await _userManager.FindByIdAsync(userId);
            listRoles = await _userManager.GetRolesAsync(user);
            return listRoles;
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<Class> classes = (from cls in _context.Classes
                                   select cls).ToList();
            List<RoleViewModel> roles = _roleManager.Roles.Select(r => new RoleViewModel()
            {
                RoleId = r.Id,
                RoleName = r.Name
            }).ToList();
            CreateUserViewModel model = new CreateUserViewModel();
            model.Classes = classes;
            model.Roles = roles;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppIdentityUser newUser = new AppIdentityUser()
                {
                    Email = model.Email,
                    ClassId = model.ClassId,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user: newUser, password: model.Password);
                if (result.Succeeded)
                {
                    string code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                    string linkConfirm = Url.Action(action: "VerifyEmail",
                                                    controller: "Account",
                                                    new { userId = newUser.Id, code = code },
                                                    Request.Scheme,
                                                    Request.Host.ToString());
                    EmailUtilities.SendConfirmEmail(newUser, linkConfirm);
                    if (model.RoleIds.Any() && model.RoleIds != null)
                    {
                        List<string> rolesName = new List<string>();
                        foreach (string roleId in model.RoleIds)
                        {
                            var role = await _roleManager.FindByIdAsync(roleId);
                            rolesName.Add(role.Name);
                        }
                        var addRoleResult = await _userManager.AddToRolesAsync(newUser, rolesName);
                        if (addRoleResult.Succeeded)
                        {
                            return RedirectToAction(actionName: "Manage");
                        }
                        foreach (var error in addRoleResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(model);
                    }

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            AppIdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel editUser = new EditUserViewModel()
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Email = user.Email,
                ClassId = user.ClassId,
                PhoneNumber = user.PhoneNumber,
                Classes = _context.Classes.Select(e => e).ToList(),
            };
            editUser.RolesName = await _userManager.GetRolesAsync(user);
            editUser.Roles = _roleManager.Roles.Select(r => new RoleViewModel()
            {
                RoleId = r.Id,
                RoleName = r.Name
            }).ToList();
            return View(editUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            AppIdentityUser user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.ClassId = model.ClassId;
            user.PhoneNumber = model.PhoneNumber;
            //when change email must confirm link in old email
            if (user.Email != model.Email)
            {
                string tokenChangeEmail = await _userManager.GenerateChangeEmailTokenAsync(user, model.Email);
                string linkConfirm = Url.Action(action: "VerifyEmailChange",
                                            controller: "Account",
                                            new { userId = user.Id, newEmail = model.Email, code = tokenChangeEmail },
                                            Request.Scheme,
                                            Request.Host.ToString());
                EmailUtilities.SendConfirmEmail(user, linkConfirm);
            }
            if (model.RoleIds.Any() && model.RoleIds != null)
            {
                List<string> rolesName = new List<string>();
                foreach (string roleId in model.RoleIds)
                {
                    var role = await _roleManager.FindByIdAsync(roleId);
                    rolesName.Add(role.Name);
                }
                IEnumerable<string> userRolesName = await _userManager.GetRolesAsync(user);
                if (userRolesName.Any() && userRolesName != null)
                {
                    var removeRoleResult = await _userManager.RemoveFromRolesAsync(user, userRolesName);
                    if (!removeRoleResult.Succeeded)
                    {
                        foreach (var error in removeRoleResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                var addRoleResult = await _userManager.AddToRolesAsync(user, rolesName);
                if (addRoleResult.Succeeded)
                {
                    return RedirectToAction(actionName: "Manage");
                }
                foreach (var error in addRoleResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
            var resultEditUser = await _userManager.UpdateAsync(user);
            if (resultEditUser.Succeeded)
            {
                return RedirectToAction(actionName: "Manage");
            }
            foreach (var error in resultEditUser.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction("Manage");
        }
    }
}
