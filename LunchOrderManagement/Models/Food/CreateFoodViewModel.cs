using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Models.Food
{
    public class CreateFoodViewModel
    {
        private string _foodName;
        private int _price;
        private bool _isActive;
        private List<IFormFile> _images;
        [Required(ErrorMessage = "Name of food is required")]
        [Display(Name = "Name of food")]
        public string FoodName { get => _foodName; set => _foodName = value; }
        [Required(ErrorMessage = "Price of food is required")]
        [Range(minimum: 1, maximum: Int32.MaxValue, ErrorMessage = "Price is not valid")]
        public int Price { get => _price; set => _price = value; }
        public bool IsActive { get => _isActive; set => _isActive = value; }
        public List<IFormFile> Images { get => _images; set => _images = value; }

        public CreateFoodViewModel()
        {
            _images = new List<IFormFile>();
        }
    }
}
