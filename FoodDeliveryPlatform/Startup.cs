using BLL;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryPlatform
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
			services.AddScoped<ICourrierManager, CourrierManager>();
			services.AddScoped<ICustomerManager, CustomerManager>();
			services.AddScoped<IDishManager, DishManager>();
			services.AddScoped<ILocationManager, LocationManager>();
			services.AddScoped<IOrderManager, OrderManager>();
			services.AddScoped<IPersonManager, PersonManager>();
			services.AddScoped<IRestaurantManager, RestaurantManager>();

			services.AddScoped<ICourriersDB, CourriersDB>();
			services.AddScoped<ICustomersDB, CustomersDB>();
			services.AddScoped<IDishesDB, DishesDB>();
			services.AddScoped<ILocationsDB, LocationsDB>();
			services.AddScoped<IOrdersDB, OrdersDB>();
			services.AddScoped<IPersonsDB, PersonsDB>();
			services.AddScoped<IRestaurantsDB, RestaurantsDB>();
			services.AddControllersWithViews();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
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

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
									name: "default",
									pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
