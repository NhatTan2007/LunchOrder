using LunchOrderManagement.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.DbContexts
{
    public class LunchOrderDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, string>
    {
        public LunchOrderDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodImage> FoodImages { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Class>().HasData(
                    new Class()
                    {
                        ClassId = Guid.NewGuid().ToString(),
                        ClassName = "C1020K1"
                    },
                    new Class()
                    {
                        ClassId = Guid.NewGuid().ToString(),
                        ClassName = "C0920G1"
                    },
                    new Class()
                    {
                        ClassId = Guid.NewGuid().ToString(),
                        ClassName = "C0221H1"
                    }
                );
            modelBuilder.Entity<Food>().HasData(
                    new Food {
                        FoodId = Guid.NewGuid().ToString(),
                        IsActive = true,
                        Name = "Cơm gà xối mỡ",
                        Price = 20000,
                    }, new Food{
                        FoodId = Guid.NewGuid().ToString(),
                        IsActive = true,
                        Name = "Cơm sườn",
                        Price = 15000
                    }, new Food {
                        FoodId = Guid.NewGuid().ToString(),
                        IsActive = true,
                        Name = "Cơm sườn non",
                        Price = 25000
                    });
            //modelBuilder.Entity<FoodImage>().HasData(
            //    new FoodImage()
            //        {
            //            FoodId = "1c00147e-a825-445f-9a7f-3143a0386bde",
            //            FileName = "com-ga-xoi-mo.jpg"
            //        },
            //        new FoodImage()
            //        {
            //            FoodId = "275eceba-0ac6-4859-9eab-37aba37fa346",
            //            FileName = "com-ga-xoi-mo.jpg"
            //        },
            //        new FoodImage()
            //        {
            //            FoodId = "546af73e-8591-4d2c-aac7-80feba7ca2dc",
            //            FileName = "com-ga-xoi-mo.jpg"
            //        }
            //    );
        }
    }
}
