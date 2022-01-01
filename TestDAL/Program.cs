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

			CourriersDB courriersDb = new CourriersDB(Configuration);
			List<int> courriers = courriersDb.GetCourriersIdByLocationId(4011);

			foreach (int c in courriers)
			{
				Console.WriteLine(c);
			}

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
			LocationsDB locationsDB = new LocationsDB(Configuration);
			List<Location> locations = locationsDB.GetLocations();

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

			LocationsDB locationsDB = new LocationsDB(Configuration);
			List<Location> locations = locationsDB.GetLocationByNPA(NPA);

			foreach (Location location in locations)
			{
				Console.WriteLine(location);
			}

			Console.WriteLine(locations);
           */


			//Test Location By Id
			/*
			LocationsDB locationsDB = new LocationsDB(Configuration);
			Location location = locationsDB.GetLocationById(10);	
			Console.WriteLine(location);
			*/

			//Test addCustomer
			//Todo controler que ça ajoute pas si id person n'existe pas.
			/*
			customersdb customersdb = new customersdb(configuration);
			customer customer = new customer();
			customer.locationid = 2;
			customer.personid = 1;
			customer = customersdb.addcustomer(customer);
			*/

			//Test addPerson
			/*
			PersonsDB personDb = new PersonsDB(Configuration);
			Person person = new Person();
			person.Firstname = "totso";
			person.Lastname = "Tatata";
			person.Login = "mylogon";
			person.Password = "asdf";

			person = personDb.AddPerson(person);
			*/

			//Test addCourrier
			/*
			CourriersDB courriersDB = new CourriersDB(Configuration);
			Courrier courrier = new Courrier();
			courrier.PersonId = 4;
			courrier = courriersDB.AddCourrier(courrier);
			*/
			//TestUpdateStatus
			/*
			OrdersDB ordersDB = new OrdersDB(Configuration);
			Order order = new Order();
			order = ordersDB.UpdateOrder(order, OrderStatusEnum.Delivering);
			*/

			//Test Location By Locality
			/*
			string Locality = "Lausanne";
			LocationsDB locationsDB = new LocationsDB(Configuration);
			Location location = locationsDB.GetLocationByLocality(Locality)[0];

		
			Console.WriteLine(location.NPA);
			*/
			//CourriersDB courriersDB = new CourriersDB(Configuration);

			//List<Courrier> courriers = courriersDB.GetCourrierByLocalite(4011, 4111);
			//foreach (Courrier courrier in courriers)
			//{
			//	Console.WriteLine(courrier.CourrierId);
			//}

		}

	}
}
