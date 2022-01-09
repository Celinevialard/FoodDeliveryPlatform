using BLL;
using DTO;
using FoodDeliveryPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace FoodDeliveryPlatform.Controllers
{
    public class RestaurantController : Controller
    {

        IRestaurantManager RestaurantManager { get; set; }
        ILocationManager LocationManager { get; set; }

        IDishManager DishManager { get; set; }

        public RestaurantController(IRestaurantManager restaurantManager, ILocationManager locationManager, IDishManager dishManager)
        {
            RestaurantManager = restaurantManager;
            LocationManager = locationManager;
            DishManager = dishManager;
        }

        /// <summary>
        /// FB
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

                var restaurants = RestaurantManager.GetRestaurantByLocation(person.CustomerInfo.LocationId);

                if (restaurants == null)
                    return View(null);


                List<RestaurantVM> restaurantsVm = new();

                foreach (var restaurant in restaurants)
                {
                    //Person customer = PersonManager.GetPersonByCustomer(order.CustomerId);
                    restaurantsVm.Add(new()
                    {
                        RestaurantId = restaurant.RestaurantId,
                        Name = restaurant.Name,
                        Description = restaurant.Description,
                        LocationName = restaurant.Location.LocationName
                    }); ;
                }

                return View(restaurantsVm);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Error", new { errorCode = HttpStatusCode.InternalServerError });
            }

        }

        /// <summary>
        /// FB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("User") == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                Restaurant restaurant = RestaurantManager.GetRestaurantById(id);
                List<Dish> dishes = DishManager.GetDishesByRestaurant(id);
                RestaurantDetailsVM restaurantDetails = new()
                {
                    RestaurantId = restaurant.RestaurantId,
                    Description = restaurant.Description,
                    Name = restaurant.Name,
                    Dishes = dishes
                };

                return View(restaurantDetails);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Error", new { errorCode = HttpStatusCode.InternalServerError });
            }

        }

    }
}
