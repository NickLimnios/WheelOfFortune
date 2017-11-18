using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WheelOfFortune.Data;
using WheelOfFortune.Models;
using WheelOfFortune.Services;
using Microsoft.Extensions.Logging;

namespace WheelOfFortune
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WheelOfFortuneContext")));
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ICouponsRepository, EFCouponRepository>();
            services.AddTransient<ITransactionRepository, EFTransactionRepository>();

            services.AddMvc();

            // Add Authorization
            services.AddAuthorization(options => { options.AddPolicy("RequireAuthenticatedUser", policy => policy.RequireAuthenticatedUser()); });
            services.AddAuthorization(options => { options.AddPolicy("RequiredAdministrator", policy => policy.RequireRole("Admin")); });
            services.AddAuthorization(options => { options.AddPolicy("RequireUser", policy => policy.RequireRole("User")); });

            services.AddDbContext<WheelOfFortuneContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("WheelOfFortuneContext")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //logging
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                  routes.MapRoute(
                      name: "Coupon",
                      template: "{controller=Coupon}/{action=CouponList}/{id?}");

                routes.MapRoute(
                      name: "Transaction",
                      template: "{controller=Transaction}/{action=TransactionList}/{id?}");

                routes.MapRoute(
                      name: "SpinTheWheelController",
                      template: "{controller=SpinTheWheel}/{action=PlayWheelOfFortune}/{id?}");

            });
            
        
        }
    }
}
