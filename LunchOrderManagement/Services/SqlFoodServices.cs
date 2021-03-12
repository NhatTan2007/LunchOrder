using LunchOrderManagement.DbContexts;
using LunchOrderManagement.Entities;
using LunchOrderManagement.Models.Food;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Services
{
    public class SqlFoodServices : IFoodServices
    {
        private readonly LunchOrderDbContext _context;
        private readonly IWebHostEnvironment _webEnv;

        public SqlFoodServices(LunchOrderDbContext context,
                                IWebHostEnvironment webEnv)
        {
            _context = context;
            _webEnv = webEnv;
        }
        public async Task<List<Food>> GetAllFoods()
        {
            //return (from food in _context.Foods
            //            where food.IsActive == true
            //            select new Food 
            //            {
            //                FoodId = food.FoodId,
            //                FoodImages = food.FoodImages,

            //            }).ToList();
            //return _context.Foods.Include(food => food.FoodImages).ToList();
            return await (from food in _context.Foods
                    .Include(f => f.FoodImages)
                    .Include(f => f.OderDetails)
                    select food
                    ).ToListAsync();
        }

        public async Task<List<Food>> GetAllFoods(string keyword)
        {
            return await (from food in _context.Foods
                    .Include(f => f.FoodImages)
                    .Include(f => f.OderDetails)
                          where food.Name.ToLower().Contains(keyword.ToLower())
                          orderby food.Name
                          select food
                    ).ToListAsync();
        }

        public async Task<List<Food>> GetActiveFoods()
        {
            return await (from food in _context.Foods
                    .Include(f => f.FoodImages)
                    .Include(f => f.OderDetails)
                    where food.IsActive == true
                    select food
                    ).ToListAsync();
        }

        public async Task<List<Food>> GetActiveFoods(string keyword)
        {
            //return (from food in _context.Foods
            //            where food.IsActive == true
            //            select new Food 
            //            {
            //                FoodId = food.FoodId,
            //                FoodImages = food.FoodImages,

            //            }).ToList();
            //return _context.Foods.Include(food => food.FoodImages).ToList();
            return await (from food in _context.Foods
                    .Include(f => f.FoodImages)
                    .Include(f => f.OderDetails)
                    where (food.IsActive == true && food.Name.ToLower().Contains(keyword.ToLower()))
                    orderby food.Name
                    select food
                    ).ToListAsync();
        }

        public async Task<Food> GetFood(string foodId)
        {
            //return _context.Foods.Include(f => f.FoodImages).SingleOrDefault(f => f.FoodId == foodId);
            return await (from food in _context.Foods
                    .Include(f => f.FoodImages)
                    .Include(f => f.OderDetails)
                    where food.FoodId == foodId
                    select food
                    ).SingleOrDefaultAsync();
        }

        public async Task<bool> CreateNewFood(Food food)
        {
            try
            {
                await _context.AddAsync(food);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> DeleteFood(Food food)
        {
            try
            {
                _context.Remove(food);
                foreach (FoodImage img in food.FoodImages.ToList())
                {
                    string filePath = Path.Combine(path1: _webEnv.WebRootPath, path2: "images", path3: "foods", img.FileName);
                    File.Delete(filePath);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> EditFood(EditFoodViewModel foodEdit)
        {
            try
            {
                Food food = await GetFood(foodEdit.FoodId);
                _context.Foods.Attach(food);
                if (foodEdit.ImagesChange.Count > 0)
                {
                    foreach (FoodImage img in food.FoodImages.ToList())
                    {
                        string filePath = Path.Combine(path1: _webEnv.WebRootPath, path2: "images", path3: "foods", img.FileName);
                        File.Delete(filePath);
                    }
                    food.FoodImages.Clear();
                    foreach (IFormFile img in foodEdit.ImagesChange)
                    {
                        string fileName = $"{Guid.NewGuid().ToString()}_{img.FileName}";
                        string filePath = Path.Combine(path1: _webEnv.WebRootPath, path2: "images", path3: "foods", fileName);
                        using (FileStream fs = new FileStream(filePath, FileMode.Create))
                        {
                            await img.CopyToAsync(fs);
                        }
                        FoodImage newImage = new FoodImage()
                        {
                            FileName = fileName,
                            ImagesId = Guid.NewGuid().ToString(),
                            FoodId = foodEdit.FoodId
                        };
                        food.FoodImages.Add(newImage);
                    }
                }
                food.IsActive = foodEdit.IsActive;
                food.Name = foodEdit.FoodName;
                food.Price = foodEdit.Price;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
