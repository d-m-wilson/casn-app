using System.Collections.Generic;
using System.Linq;
using CASNApp.Admin.Data;
using CASNApp.Core;
using CASNApp.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;

namespace CASNApp.Admin
{
    public class Startup
    {
        public static IReadOnlyList<string> ControllerNames { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var controllerNames = System.Reflection.Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Microsoft.AspNetCore.Mvc.Controller))
                    && t.GetMethod("Index") != null)
                .Select(t => t.Name.Replace("Controller", ""))
                .OrderBy(x => x)
                .ToList();

            ControllerNames = controllerNames;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddNLog();
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(Constants.DbConnectionString), sqlOptions =>
                {
                    sqlOptions
                        .EnableRetryOnFailure(int.Parse(Configuration[Constants.DBRetryCount]));
                }), ServiceLifetime.Scoped, ServiceLifetime.Scoped);

            services.AddDbContext<casn_appContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString(Constants.DbConnectionString), sqlOptions =>
                    {
                        sqlOptions
                            .EnableRetryOnFailure(int.Parse(Configuration[Constants.DBRetryCount]));
                    });
                }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication()
                .AddGoogle(options =>
            {
                var googleAuthSection = Configuration.GetSection("Authentication:Google");

                options.ClientId = googleAuthSection["ClientId"];
                options.ClientSecret = googleAuthSection["ClientSecret"];
            });

            services.AddAuthorization();

            services.AddControllersWithViews();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
