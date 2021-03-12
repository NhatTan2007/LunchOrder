using LunchOrderManagement.Entities;
using LunchOrderManagement.Models.Order;
using LunchOrderManagement.Models.Pagination;
using LunchOrderManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LunchOrderManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<AppIdentityUser> _signinManager;
        private readonly IFoodServices _foodServices;
        private readonly IOrderServices _orderServices;

        public HomeController(SignInManager<AppIdentityUser> signinManager,
                                IFoodServices foodServices,
                                    IOrderServices orderServices)
        {
            _signinManager = signinManager;
            _foodServices = foodServices;
            _orderServices = orderServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page, int pageSize, string keyword)
        {
            List<Food> foods;
            if (keyword == null)
            {
                foods = await _foodServices.GetActiveFoods();
            }
            else
            {
                foods = await _foodServices.GetActiveFoods(keyword);
                ViewBag.keyword = keyword;
            }
            if (page == 0) page = 1;
            if (pageSize == 0) pageSize = 8;
            Pager pager = new Pager(totalItems: foods.Count, currentPage: page, pageSize: pageSize);
            PaginationViewModel<Food> model = new PaginationViewModel<Food>()
            {
                Pager = pager,
                Items = foods.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize)
            };
            ViewBag.pageSize = pageSize;
            ViewBag.keyword = keyword;
            return View(model);
        }

        [Authorize(Roles = "System Administrator, Manager")]
        public async Task<IActionResult> Admin()
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            List<OrderDetail> orders = await _orderServices.GetAllOrdersToday();
            List<FoodOrderCountViewModel> report = orders.GroupBy(o => new 
            {
                o.FoodId,
                o.Food.Name,
                o.Food.Price
            }).Select(g => new FoodOrderCountViewModel
            {
                FoodId = g.Key.FoodId,
                FoodName = g.Key.Name,
                Count = g.Count(),
                Price = g.Key.Price
                
            }).ToList();
            return View(report);
            
        }
    }
}
