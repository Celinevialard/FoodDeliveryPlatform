using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using DAL;
using DTO;
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

			/*
			RestaurantsDB restaurantDb = new RestaurantsDB(Configuration);

			List<Restaurant> restaurants =  restaurantDb.GetRestaurantsByLocalite(4011);

			foreach (Restaurant r in restaurants)
			{
				Console.WriteLine(r);
			}
			*/


			//Test localite
			/*
			LocationDB locationDB = new LocationDB(Configuration);
			List<Location> locations = locationDB.GetLocations();

            foreach (Location location in locations)
            {
                Console.WriteLine(location);
            }
			*/

			//Test Location By NPA
			/*
			string NPA;
            Console.WriteLine("Saisir le NPA : ");
			NPA = Console.ReadLine();

			LocationDB locationDB = new LocationDB(Configuration);
			List<Location> locations = locationDB.GetLocationByNPA(NPA);

			foreach (Location location in locations)
			{
				Console.WriteLine(location);
			}

			Console.WriteLine(locations);
           */


			//Test Location By Id
			/*
			LocationDB locationDB = new LocationDB(Configuration);
			Location location = locationDB.GetLocationById(10);	
			Console.WriteLine(location);
			*/

			//Test addCustomer
			//Todo controler que ça ajoute pas si id person n'existe pas.
			CustomerDB customerDB = new CustomerDB(Configuration);
			Customer customer = new Customer();
			customer.LocationId = 2;
			customer.PersonId = 1;
			customer = customerDB.AddCustomer(customer);

		}
	}
}
