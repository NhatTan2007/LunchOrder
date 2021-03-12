using LunchOrderManagement.Entities;
using LunchOrderManagement.Models.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Services
{
    public interface IFoodServices
    {
        public Task<List<Food>> GetAllFoods();
        public Task<List<Food>> GetAllFoods(string keyword);
        public Task<List<Food>> GetActiveFoods();
        public Task<List<Food>> GetActiveFoods(string keyword);
        public Task<Food> GetFood(string foodId);
        public Task<bool> CreateNewFood(Food food);

        public Task<bool> EditFood(EditFoodViewModel foodEdit);
        public Task<bool> DeleteFood(Food food);
    }
}
