using LunchOrderManagement.Entities;
using LunchOrderManagement.Models.Food;
using LunchOrderManagement.Models.Pagination;
using LunchOrderManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Controllers
{
    [Authorize]
    public class FoodController : Controller
    {
        private readonly IFoodServices _foodServices;
        private readonly IWebHostEnvironment _webEnv;

        public FoodController(IFoodServices foodServices,
                                IWebHostEnvironment webEnv)
        {
            _foodServices = foodServices;
            _webEnv = webEnv;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateFoodViewModel model)
        {
            if (ModelState.IsValid)
            {
                Food newFood = new Food()
                {
                    FoodId = Guid.NewGuid().ToString(),
                    IsActive = model.IsActive,
                    Name = model.FoodName,
                    Price = model.Price
                };
                string folderPath = Path.Combine(path1:_webEnv.WebRootPath, path2: "images", path3: "foods");
                string fileName = String.Empty;
                string filePath = String.Empty;
                List<FoodImage> images = new List<FoodImage>();
                if (model.Images.Count > 0)
                {
                    foreach (IFormFile img in model.Images)
                    {
                        fileName = $"{Guid.NewGuid()}_{img.FileName}";
                        filePath = Path.Combine(path1: folderPath, path2: fileName);
                        using (FileStream fs = new FileStream(filePath, FileMode.Create))
                        {
                            await img.CopyToAsync(fs);
                            FoodImage image = new FoodImage()
                            {
                                ImagesId = Guid.NewGuid().ToString(),
                                FoodId = newFood.FoodId,
                                FileName = fileName,
                            };
                            images.Add(image);
                        }
                    }
                    newFood.FoodImages = new List<FoodImage>(images);
                }
                if (await _foodServices.CreateNewFood(newFood))
                {
                    return RedirectToAction(actionName: "Manage", controllerName: "Food");
                }
                ModelState.AddModelError("", "Something was wrong, please try again later");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string foodId)
        {
            Food food = await _foodServices.GetFood(foodId);
            if (food == null)
            {
                return NotFound();
            }
            EditFoodViewModel foodEdit = new EditFoodViewModel()
            {
                FoodName = food.Name,
                Images = food.FoodImages.ToList(),
                IsActive = food.IsActive,
                Price = food.Price,
                FoodId = food.FoodId
            };
            return View(foodEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditFoodViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _foodServices.EditFood(model))
                {
                    return RedirectToAction(actionName: "Profile", controllerName: "Food", new { foodId = model.FoodId });
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string foodId)
        {
            Food delFood = await _foodServices.GetFood(foodId);
            if (delFood == null) return NotFound();
            await _foodServices.DeleteFood(delFood);
            return RedirectToAction(actionName: "Manage", controllerName: "Food");
        }
        [HttpGet]
        public async Task<IActionResult> Profile(string foodId)
        {
            Food food = await _foodServices.GetFood(foodId);
            if (food == null) return NotFound();
            FoodViewModel model = new FoodViewModel()
            {
                FoodName = food.Name,
                IsActive = food.IsActive,
                Price = food.Price,
                FoodId = food.FoodId
            };
            if (food.FoodImages != null)
            {
                model.Images = food.FoodImages.ToList();
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Manage(int page, int pageSize, string keyword)
        {
            List<Food> foods;
            if (keyword == null)
            {
                foods = await _foodServices.GetAllFoods();
            }
            else
            {
                foods = await _foodServices.GetAllFoods(keyword);
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
    }
}
