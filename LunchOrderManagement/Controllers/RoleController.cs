using LunchOrderManagement.Entities;
using LunchOrderManagement.Models.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppIdentityRole> _roleManager;

        public RoleController(RoleManager<AppIdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Manage()
        {
            List<RoleViewModel> roles = _roleManager.Roles.Select(r => new RoleViewModel
            {
                RoleId = r.Id,
                RoleName = r.Name
            }).ToList();
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppIdentityRole newRole = new AppIdentityRole()
                {
                    Name = model.RoleName
                };
                var result = await _roleManager.CreateAsync(newRole);
                if (result.Succeeded)
                {
                    return RedirectToAction(actionName: "Manage");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string roleId)
        {
            AppIdentityRole role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }
            EditRoleViewModel roleEdit = new EditRoleViewModel()
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
            return View(roleEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppIdentityRole role = await _roleManager.FindByIdAsync(model.RoleId);
                if (role == null)
                {
                    return NotFound();
                }
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(actionName: "Manage");
                }
                foreach (IdentityError  error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string roleId)
        {
            AppIdentityRole roleDel = await _roleManager.FindByIdAsync(roleId);
            var result = await _roleManager.DeleteAsync(roleDel);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Something was wrong, please try again");
            }
            return RedirectToAction(actionName: "Manage");
        }

    }
}
