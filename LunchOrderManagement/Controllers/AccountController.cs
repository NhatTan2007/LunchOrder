using LunchOrderManagement.DbContexts;
using LunchOrderManagement.Entities;
using LunchOrderManagement.Models.Account;
using LunchOrderManagement.Models.Role;
using LunchOrderManagement.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly LunchOrderDbContext _context;
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly RoleManager<AppIdentityRole> _roleManager;

        public AccountController(LunchOrderDbContext context,
                                    UserManager<AppIdentityUser> userManager,
                                        SignInManager<AppIdentityUser> signInManager,
                                            RoleManager<AppIdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppIdentityUser userLogin = await _userManager.FindByEmailAsync(model.Email);
                if (userLogin == null)
                {
                    return NotFound();
                }
                var result = await _signInManager.PasswordSignInAsync(user: userLogin,
                                                                password: model.Password,
                                                                isPersistent: model.IsRemember,
                                                                lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }
                ModelState.AddModelError("", "Something was wrong, please check again");

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterAccountViewModel();
            model.Classes = _context.Classes.Select(e => e).ToList();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppIdentityUser newUser = new AppIdentityUser()
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    ClassId = model.ClassId
                };
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user: newUser, role: "Normal User");
                    string code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                    string linkConfirm = Url.Action(action: "VerifyEmail",
                                                    controller: "Account",
                                                    new { userId = newUser.Id, code = code },
                                                    Request.Scheme,
                                                    Request.Host.ToString());
                    EmailUtilities.SendConfirmEmail(newUser, linkConfirm);
                    return RedirectToAction(actionName: "SentConfirmEmail");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            model.Classes = _context.Classes.Select(e => e).ToList();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Profile(string userId)
        {
            AppIdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            AccountViewModel model = new AccountViewModel()
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Email = user.Email,
                ClassName = _context.Classes.Where(c => c.ClassId == user.ClassId).Select(e => e.ClassName).SingleOrDefault(),
                PhoneNumber = user.PhoneNumber
            };
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
            EditAccountViewModel editUser = new EditAccountViewModel()
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Email = user.Email,
                ClassId = user.ClassId,
                PhoneNumber = user.PhoneNumber,
                Classes = _context.Classes.Select(e => e).ToList()
            };
            return View(editUser);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditAccountViewModel model)
        {
            AppIdentityUser user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.ClassId = model.ClassId;
            model.Classes = _context.Classes.Select(e => e).ToList();
            if (String.IsNullOrEmpty(model.PhoneNumber)) model.PhoneNumber = user.PhoneNumber;
            if (String.IsNullOrEmpty(model.Email)) model.Email = user.Email;
            var compareUser = await _userManager.FindByEmailAsync(model.Email);
            if(compareUser != null && compareUser.Id != user.Id)
            {
                ModelState.AddModelError("", errorMessage: "Email is used by another account");

                return View(model);
            }
            //when change email must confirm link in old email
            if (user.Email != model.Email)
            {
                
                string tokenChangeEmail = await _userManager.GenerateChangeEmailTokenAsync(user, model.Email);
                string linkConfirm = Url.Action(action: "VerifyEmailChange",
                                            controller: "Account",
                                            new { userId = user.Id, newEmail = model.Email, code = tokenChangeEmail },
                                            Request.Scheme,
                                            Request.Host.ToString());
                EmailUtilities.SendConfirmChangeEmail(user, linkConfirm);
                ViewData["Sendmail"] = "We have sent an email to confirm your action";
            }
            //when change phone number must confirm link in mail
            if (model.PhoneNumber != user.PhoneNumber && !String.IsNullOrEmpty(model.PhoneNumber))
            {
                string tokenPhoneNumber = await _userManager.GenerateChangePhoneNumberTokenAsync(user, model.PhoneNumber);
                string linkConfirm = Url.Action(action: "VerifyPhoneNumberChange",
                            controller: "Account",
                            new { userId = user.Id, newPhoneNumber = model.PhoneNumber, code = tokenPhoneNumber },
                            Request.Scheme,
                            Request.Host.ToString());
                EmailUtilities.SendConfirmChangePhoneNumber(user, linkConfirm);
                ViewData["Sendmail"] = "We have sent an email to confirm your action";
            }
            if (String.IsNullOrEmpty(model.OldPassword) && !String.IsNullOrEmpty(model.NewPassword))
            {
                ModelState.AddModelError("", errorMessage: "If you want to change password, please enter old password correctly");
                return View(model);
            }
            bool check = await _userManager.CheckPasswordAsync(user, model.OldPassword);
            bool check1 = !String.IsNullOrEmpty(model.NewPassword);
            if (await _userManager.CheckPasswordAsync(user, model.OldPassword) && !String.IsNullOrEmpty(model.NewPassword))
            {
                var resultChangePass = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (resultChangePass.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }
                foreach (var error in resultChangePass.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
            if (await _userManager.CheckPasswordAsync(user, model.OldPassword) == false && !String.IsNullOrEmpty(model.NewPassword))
            {
                ModelState.AddModelError("", "Wrong current password");
                return View(model);
            }
            var resultEditUser = await _userManager.UpdateAsync(user);
            if (resultEditUser.Succeeded)
            {
                return RedirectToAction(actionName: "Profile", new { userId = model.UserId});
            }
            foreach (var error in resultEditUser.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string userId, string code)
        {
            AppIdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            if (!user.EmailConfirmed)
            {
                try
                {
                    IdentityResult result = await _userManager.ConfirmEmailAsync(user, code);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user: user, isPersistent: false);
                        return RedirectToAction(actionName: "Index", controllerName: "Home");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> VerifyEmailChange(string userId, string newEmail, string code)
        {
            AppIdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            try
            {
                IdentityResult result = await _userManager.ChangeEmailAsync(user, newEmail, code);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user: user, isPersistent: false);
                    return RedirectToAction(actionName: "ChangeConfirm");
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> VerifyPhoneNumberChange(string userId, string newPhoneNumber, string code)
        {
            AppIdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();
            try
            {
                IdentityResult result = await _userManager.ChangePhoneNumberAsync(user, newPhoneNumber, code);
                if (result.Succeeded)
                {
                    return RedirectToAction(actionName: "ChangeConfirm");
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult SentConfirmEmail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangeConfirm()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Signout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        public async Task<IActionResult> EmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            return Json($"{email} is used");
        }
    }
}
