using BLL;
using FoodDeliveryPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryPlatform.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		IPersonManager PersonManager { get; set; }

		public HomeController(ILogger<HomeController> logger, IPersonManager personManager)
		{
			_logger = logger;
			PersonManager = personManager;
		}

		public IActionResult Index()
		{
			return View();
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
			HttpContext.Session.SetString("User", person.ToString()); 

			if(person.CustomerInfo != null)
				return RedirectToAction("Index", "Order");
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
