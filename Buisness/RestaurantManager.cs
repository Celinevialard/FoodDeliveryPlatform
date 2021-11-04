using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BLL
{
    class RestaurantManager
    {
        private RestaurantsDB RestaurantsDb{ get; }
        private LocationsDB LocationsDb { get; }
        public RestaurantManager(IConfiguration conf)
        {
            RestaurantsDb = new RestaurantsDB(conf);
            LocationsDb = new LocationsDB(conf);
        }
        
        public List<Restaurant> GetRestaurantByLocation(string locality)
        {
            Location location = new Location();
            location = LocationsDb.GetLocationByLocality(locality);

            List<Restaurant> restaurants = new List<Restaurant>();
            restaurants = RestaurantsDb.GetRestaurantsByLocation(location.LocationId);
            return restaurants;
            
        }
        
    }
}
