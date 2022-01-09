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
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoodDeliveryPlatform.Controllers
{
    /// <summary>
    /// url : http://153.109.124.35:81/CV_FB_FoodDelivery
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

        /// <summary>
        /// Selection de la page d'accueil
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            try
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
            catch (Exception e)
            {
                return RedirectToAction("Index", "Error", new
                {
                    errorCode = HttpStatusCode.InternalServerError
                });
            }
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

        /// <summary>
        /// Edition d'un user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("User") == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                UserVM person = JsonSerializer.Deserialize<UserVM>(HttpContext.Session.GetString("User"));

                if (person.PersonId != id || person.CustomerInfo == null)
                {
                    return RedirectToAction("Index", "Error", new { errorCode = 403 });
                }

                List<Location> locations = LocationManager.GetLocations();
                Person personFromDb = PersonManager.GetPersonByCustomer(person.CustomerInfo.CustomerId);


                return View(new UserEditVM
                {
                    PersonId = personFromDb.PersonId,
                    Firstname = personFromDb.Firstname,
                    Lastname = personFromDb.Lastname,
                    Address = personFromDb.CustomerInfo.Address,
                    LocationId = personFromDb.CustomerInfo.LocationId,
                    Email = personFromDb.Login,
                    Password = personFromDb.Password,
                    Locations = locations
                });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Error", new
                {
                    errorCode = HttpStatusCode.InternalServerError
                });
            }
        }

        /// <summary>
        /// Edition d'un user
        /// </summary>
        /// <param name="editUser"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(UserEditVM editUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    List<Location> locations = LocationManager.GetLocations();
                    editUser.Locations = locations;
                    return View(editUser);
                }

                CustomerManager.UpdateCustomer(new Person
                {
                    CustomerInfo = new Customer
                    {
                        Address = editUser.Address,
                        LocationId = editUser.LocationId
                    },
                    PersonId = editUser.PersonId,
                    Firstname = editUser.Firstname,
                    Lastname = editUser.Lastname,
                    Login = editUser.Email,
                    Password = editUser.Password
                });

                return RedirectToAction("Logout");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Error", new
                {
                    errorCode = HttpStatusCode.InternalServerError
                });
            }
        }

        /// <summary>
        /// Création d'un user
        /// </summary>
        /// <returns></returns>
        public IActionResult SignUp()
        {
            try
            {
                List<Location> locations = LocationManager.GetLocations();
                return View(new UserEditVM { Locations = locations });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Error", new
                {
                    errorCode = HttpStatusCode.InternalServerError
                });
            }
        }

        /// <summary>
        /// Création d'un user
        /// </summary>
        /// <param name="signUp"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SignUp(UserEditVM signUp)
        {
            try
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
            catch (Exception e)
            {
                return RedirectToAction("Index", "Error", new
                {
                    errorCode = HttpStatusCode.InternalServerError
                });
            }
        }

        /// <summary>
        /// Connection d'un user
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Connection d'un user
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginVM login)
        {
            try
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

                if (person.CustomerInfo != null)
                    return RedirectToAction("Index", "Restaurant");
                if (person.CourrierInfo != null)
                    return RedirectToAction("Index", "Order");
                return RedirectToAction("Index", "Restaurant");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Error", new { errorCode = HttpStatusCode.InternalServerError });
            }
        }

        /// <summary>
        /// Deconnection d'un user
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
