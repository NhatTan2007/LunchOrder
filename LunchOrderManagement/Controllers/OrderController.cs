using LunchOrderManagement.DbContexts;
using LunchOrderManagement.Entities;
using LunchOrderManagement.Services;
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
    public class OrderController : Controller
    {
        private readonly SignInManager<AppIdentityUser> _signInManager;
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly IFoodServices _foodServices;
        private readonly IOrderServices _orderServices;

        public OrderController(SignInManager<AppIdentityUser> signInManager,
                                UserManager<AppIdentityUser> userManager,
                                IFoodServices foodServices,
                                IOrderServices orderServices)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _foodServices = foodServices;
            _orderServices = orderServices;
        }
        [HttpGet]
        public async Task<IActionResult> Create(string foodId)
        {
            AppIdentityUser user = await _userManager.GetUserAsync(User);
            Food food = await _foodServices.GetFood(foodId);
            if (user != null && food != null)
            {
                OrderDetail order = await _orderServices.GetOrderToday(user.Id);
                if (order == null)
                {
                    if(await _orderServices.CreateOrder(food: food, user: user))
                    {
                        ViewBag.foodName = food.Name;
                        return View("~/Views/Order/OrderSuccess.cshtml");
                    }
                }
            }
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string foodId)
        {
            AppIdentityUser user = await _userManager.GetUserAsync(User);
            Food food = await _foodServices.GetFood(foodId);
            if (user != null && food != null)
            {
                OrderDetail order = await _orderServices.GetOrderToday(user.Id);
                if (order != null)
                {
                    if (order.FoodId != foodId)
                    {
                        if (await _orderServices.DeleteOrder(order) && await _orderServices.CreateOrder(food: food, user: user))
                        {
                            ViewBag.foodName = food.Name;
                            return View("~/Views/Order/EditSuccess.cshtml");
                        }
                    }
                }
            }
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Cancel(string userId)
        {
            OrderDetail order = await _orderServices.GetOrderToday(userId);
            if (order != null)
            {
                await _orderServices.DeleteOrder(order);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
