using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Models.Order
{
    public class FoodOrderCountViewModel
    {
        private string _foodId;
        private string _foodName;
        private int _count;
        private int _price;

        public string FoodId { get => _foodId; set => _foodId = value; }
        public string FoodName { get => _foodName; set => _foodName = value; }
        public int Count { get => _count; set => _count = value; }
        public int Price { get => _price; set => _price = value; }
        public int Amount => _count * _price;
    }
}
