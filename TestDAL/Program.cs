﻿using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using DAL;
using System.Collections.Generic;

namespace TestDAL
{
	class Program
	{

		public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				 .Build();

		static void Main(string[] args)
		{
			RestaurantsDB restaurantDb = new RestaurantsDB(Configuration);

			List<Restaurant> restaurants =  restaurantDb.GetRestaurantsByLocalite(4011);

			foreach (Restaurant r in restaurants)
			{
				Console.WriteLine(r);
			}
		}
	}
}
