using LunchOrderManagement.DbContexts;
using LunchOrderManagement.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Services
{
    public class SqlOrderServices : IOrderServices
    {
        private readonly LunchOrderDbContext _context;

        public SqlOrderServices(LunchOrderDbContext context)
        {
            _context = context;
        }
        public async Task<OrderDetail> GetOrderToday(string userId)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            return await (from ord in _context.OrderDetails
                    .Include(o => o.Food)
                    .Include(u => u.User)
                    where (ord.UserId == userId && ord.DateOrder == date)
                    select ord
                    ).SingleOrDefaultAsync();
        }

        public async Task<List<OrderDetail>> GetAllOrdersToday()
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            return await (from ord in _context.OrderDetails
                            .Include(o => o.Food)
                            .Include(u => u.User)
                            where ord.DateOrder == date
                            select ord
                            ).ToListAsync();
        }

        public async Task<bool> CreateOrder(Food food, AppIdentityUser user)
        {
            try
            {
                OrderDetail newOrder = new OrderDetail()
                {
                    DateOrder = DateTime.Now.ToString("dd/MM/yyyy"),
                    FoodId = food.FoodId,
                    UserId = user.Id,
                    OderDetailId = Guid.NewGuid().ToString()
                };
                await _context.OrderDetails.AddAsync(newOrder);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteOrder(OrderDetail order)
        {
            try
            {
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                _context.OrderDetails.Remove(order);
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
