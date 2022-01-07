using BLL;
using DTO;
using FoodDeliveryPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoodDeliveryPlatform.Controllers
{
	/// <summary>
	/// http://153.109.124.35:81/CV_FB_FoodDelivery
	/// </summary>
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		IPersonManager PersonManager { get; set; }
		ICustomerManager CustomerManager { get; set; }
		ILocationManager LocationManager { get; set; }

		public HomeController(ILogger<HomeController> logger, IPersonManager personManager, ILocationManager locationManager, ICustomerManager customerManager)
		{
			_logger = logger;
			PersonManager = personManager;
			LocationManager = locationManager;
			CustomerManager = customerManager;
		}

		public IActionResult Index()
		{
			if (HttpContext.Session.GetString("User") == null)
			{
				return RedirectToAction("Login", "Home");
			}
			UserVM person = JsonSerializer.Deserialize<UserVM>(HttpContext.Session.GetString("User"));
			
			if (person == null)
				return RedirectToAction("Logout", "Home");

			if (person.CustomerInfo != null)
				return RedirectToAction("Index", "Restaurant");

			if (person.CourrierInfo != null)
				return RedirectToAction("Index", "Order");

			return RedirectToAction("Logout", "Home");
		}

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Edit(int id)
		{

			List<Location> locations = LocationManager.GetLocations();
			return View(new UserEditVM { Locations = locations });
		}

		[HttpPost]
		public IActionResult Edit(UserEditVM editUser)
		{
			if (!ModelState.IsValid)
			{
				List<Location> locations = LocationManager.GetLocations();
				editUser.Locations = locations;
				return View(editUser);
			}

			CustomerManager.AddCustomer(new Person
			{
				CustomerInfo = new Customer
				{
					PersonId = editUser.PersonId,
					Address = editUser.Address,
					LocationId = editUser.LocationId
				},
				Firstname = editUser.Firstname,
				Lastname = editUser.Lastname,
				Login = editUser.Email,
				Password = editUser.Password
			});

			return RedirectToAction("Login");
		}

		public IActionResult SignUp()
        {
			List<Location> locations = LocationManager.GetLocations();
			return View(new UserEditVM { Locations = locations});
		}

		[HttpPost]
		public IActionResult SignUp(UserEditVM signUp)
		{
			if (!ModelState.IsValid)
            {
				List<Location> locations = LocationManager.GetLocations();
				signUp.Locations = locations;
				return View(signUp);
			}

			CustomerManager.AddCustomer(new Person
			{
				CustomerInfo = new Customer
				{
					Address = signUp.Address,
					LocationId = signUp.LocationId
				},
				Firstname = signUp.Firstname,
				Lastname = signUp.Lastname,
				Login = signUp.Email,
				Password = signUp.Password
			});

			return RedirectToAction("Login");
		}
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginVM login)
		{
			if (!ModelState.IsValid)
				return View(login);

			var person = PersonManager.GetPersonByLogin(login.Email, login.Password);
			if (person == null)
			{
				ModelState.AddModelError(string.Empty, "Invalid email or password");
				return View(login);
			}
			UserVM user = new()
			{
				CourrierInfo = person.CourrierInfo,
				CustomerInfo = person.CustomerInfo,
				Firstname = person.Firstname,
				Lastname = person.Lastname,
				PersonId = person.PersonId
			};
			HttpContext.Session.SetString("User", user.ToString()); 

			if(person.CustomerInfo != null)
				return RedirectToAction("Index", "Restaurant");
			if (person.CourrierInfo != null)
				return RedirectToAction("Index", "Order");
			return RedirectToAction("Index", "Restaurant");
		}

		


		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login");
		}
	}
}
