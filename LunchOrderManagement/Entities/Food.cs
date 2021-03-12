using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Entities
{
    public class Food
    {
        private string _foodId;
        private string _name;
        private int _price;
        private bool _isActive;
        [Key]
        public string FoodId { get => _foodId; set => _foodId = value; }
        [Required]
        public string Name { get => _name; set => _name = value; }
        [Required]
        public int Price { get => _price; set => _price = value; }
        [Required]
        public bool IsActive { get => _isActive; set => _isActive = value; }

        public ICollection<FoodImage> FoodImages { get; set; }
        public ICollection<OrderDetail> OderDetails { get; set; }
    }
}
