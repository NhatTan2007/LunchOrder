using LunchOrderManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Services
{
    public interface IOrderServices
    {
        public Task<bool> CreateOrder(Food food, AppIdentityUser user);
        public Task<OrderDetail> GetOrderToday(string userId);
        public Task<List<OrderDetail>> GetAllOrdersToday();
        public Task<bool> DeleteOrder(OrderDetail order);
    }
}
