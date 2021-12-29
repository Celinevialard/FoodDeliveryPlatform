using BLL;
using DTO;
using FoodDeliveryPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace FoodDeliveryPlatform.Controllers
{
    public class RestaurantController : Controller
    {

        IRestaurantManager RestaurantManager { get; set; }
        ILocationManager LocationManager { get; set; }
      
        public RestaurantController(IRestaurantManager restaurantManager, ILocationManager locationManager)
        {
            RestaurantManager = restaurantManager;
            LocationManager = locationManager;
        }

        /// <summary>
        /// FB
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            UserVM person = JsonSerializer.Deserialize<UserVM>(HttpContext.Session.GetString("User"));

            var restaurants = RestaurantManager.GetRestaurantByLocation(person.CustomerInfo.LocationId);

            if (restaurants == null)
                return View(null);


            List<RestaurantVM> restaurantsVm = new();
            
            foreach (var restaurant in restaurants)
            {
                //Person customer = PersonManager.GetPersonByCustomer(order.CustomerId);
                restaurantsVm.Add(new()
                {
                    Name = restaurant.Name,
                    Description = restaurant.Description,
                    LocationName = restaurant.Location.LocationName
                }); ;
            }

           

            return View(restaurantsVm);
           
        }

        /// <summary>
        /// FB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
