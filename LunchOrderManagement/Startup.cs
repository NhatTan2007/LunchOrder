using LunchOrderManagement.DbContexts;
using LunchOrderManagement.Entities;
using LunchOrderManagement.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;
        public Startup(IWebHostEnvironment env,
                        IConfiguration config)
        {
            _env = env;
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddTransient<IFoodServices, SqlFoodServices>();
            services.AddTransient<IOrderServices, SqlOrderServices>();
            services.AddDbContext<LunchOrderDbContext>(option 
                                                        => option.UseSqlServer(_config.GetConnectionString("DBConnection")));
            services.AddIdentity<AppIdentityUser, AppIdentityRole>(opt =>
                    {
                        opt.User.RequireUniqueEmail = true;
                        opt.SignIn.RequireConfirmedEmail = true;
                    })
                    .AddEntityFrameworkStores<LunchOrderDbContext>()
                    .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStatusCodePagesWithReExecute("/error404");
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/error404");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routers =>
            {
                routers.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{page?}/{pageSize?}/{keyword?}");
            });

            app.Run(context =>
            {
                context.Response.StatusCode = 404;
                return Task.FromResult(0);
            });


        }
    }
}
