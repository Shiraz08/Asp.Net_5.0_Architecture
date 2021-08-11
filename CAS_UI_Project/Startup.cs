using CAS_UI_Project.Areas.Identity.Data;
using CAS_UI_Project.Data;
using CAS_UI_Project.Models.EmailSender;
using CAS_UI_Project.Models.HealthCheck;
using HealthChecks.UI.Client;
using IdentityApplication.Models.Roles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAS_UI_Project
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
            services.AddDbContext<IdentitysContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<IdentitysContext>()
        .AddDefaultUI()
               .AddDefaultTokenProviders();
            services.AddControllersWithViews();

            //email sender override class
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddControllersWithViews();

            //Policy for claims
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequiredPolicy", policy =>
                    policy.RequireClaim("canadduser", "canadduser"));
            });

            services.AddRazorPages();
            //Facebook Login Authentication
            services.AddAuthentication().AddFacebook(o =>
            {
                o.AppId = "546100403078193";
                o.AppSecret = "5b1632c677fcfb0530dd732accc47b59";
            });
            //Google Login Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
           .AddCookie(options =>
           {
               options.LoginPath = "/Account/google-login"; 
           })
           .AddGoogle(options =>
           {
               options.ClientId = "1058201086198-4kchdk9920hpbtf5626mblplr6cisimq.apps.googleusercontent.com";
               options.ClientSecret = "bkf5zupI2oOJ2GllkFfN3U5C";
           });
            //Health Check           
            services.AddHealthChecks()
                         .AddSqlServer(Configuration["ConnectionStrings:DefaultConnection"])
                        .AddDiskStorageHealthCheck(s => s.AddDrive("C:\\", 1024), "Disk Storage") // 1024 MB (1 GB) free minimum
                        .AddProcessAllocatedMemoryHealthCheck(512, "Allocate Memory") // 512 MB max allocated memory
                        .AddProcessHealthCheck("ProcessName", p => p.Length > 0) // check if process is running // check if a windows service is running
                        .AddUrlGroup(new Uri("https://www.gitmemory.com/CarlosLanderas"), "URL");

            services
                .AddHealthChecksUI(s =>
                {
                    s.AddHealthCheckEndpoint("Application Health", "https://localhost:44341/health");
                })
                .AddInMemoryStorage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

                endpoints.MapHealthChecksUI();

                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
