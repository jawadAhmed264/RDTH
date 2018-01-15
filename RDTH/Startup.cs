using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RDTH.Data;
using RDTH.Models;
using RDTH.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using RDTH.Service;

namespace RDTH
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
            services.AddDbContext<RDTHDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<RDTHDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton(Configuration);
           
            //Add Services Here

            services.AddScoped<ISetBoxService,SetBoxService>();
            services.AddScoped<IPackageService,PackageService>();
            services.AddScoped<ISubscribeService, SubscribeService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IFeedback, FeedbackService>();
            services.AddScoped<IFaq, FaqService>();
            services.AddScoped<IDealerService, DealerService>();
            services.AddScoped<IDistributerService, DistributerServicecs>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICustomerPackage, CustomerPackageService>();
            services.AddScoped<ICustomer, CustomerService>();
            services.AddScoped<IMod, ModService>();
            services.AddScoped<IRechargeService, RechargeService>();
            services.AddScoped<INewSetBoxRequest, NewSetBoxRequestService>();
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
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
                    name:"areaRoute", 
                    template:"{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider);
            
        }

        private void CreateRoles(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            Task<IdentityResult> roleResult;
            string email = "jawad@yahoo.com";
            string[] roleNames = { "Admin", "User", "Dealer","Distributer" };

            //Check that there is an Administrator role and create if not
            foreach (var roleName in roleNames)
            {
                Task<bool> hasAdminRole = roleManager.RoleExistsAsync(roleName);
                hasAdminRole.Wait();

                if (!hasAdminRole.Result)
                {
                    roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                    roleResult.Wait();
                }
            }
            //Check if the admin user exists and create it if not
            //Add to the Administrator role

            Task<ApplicationUser> testUser = userManager.FindByEmailAsync(email);
            testUser.Wait();

            if (testUser.Result == null)
            {
                ApplicationUser administrator = new ApplicationUser();
                administrator.Email = email;
                administrator.UserName = email;

                string pass = "Aa@12345";
                Task<IdentityResult> newUser = userManager.CreateAsync(administrator, pass);
                newUser.Wait();

                if (newUser.Result.Succeeded)
                {
                    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(administrator, "Admin");
                    newUserRole.Wait();
                }
            }

        }
    }

}
