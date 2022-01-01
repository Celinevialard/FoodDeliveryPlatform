﻿using BLL;
using DTO;
using FoodDeliveryPlatform.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// FB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction("Login", "Home");
            }


           
            List<Dish> dishes = new List<Dish>();

            dishes.AddRange(DishManager.GetDishesByRestaurant(id));


            List<DishVM> dishesVM = new();

            foreach (var dish in dishes)
            {

                dishesVM.Add(new()
                {
                    Name = dish.Name,
                    Description = dish.Description,
                    Price = dish.Price,
                    Allergies = dish.Allergies    
                })  ;
            }


            return View(dishesVM);
            
        }

    }
}
