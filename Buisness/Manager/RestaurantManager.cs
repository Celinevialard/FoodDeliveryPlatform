using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class RestaurantManager : IRestaurantManager
    {
        private IRestaurantsDB RestaurantsDb { get; }
        private ILocationsDB LocationsDb { get; }
        public RestaurantManager(IConfiguration conf)
        {
            RestaurantsDb = new RestaurantsDB(conf);
            LocationsDb = new LocationsDB(conf);
        }

        /// <summary>
        /// Liste les restaurants en fonction de la localite saisie
        /// </summary>
        /// <param name="locality"></param>
        /// <returns></returns>
        public List<Restaurant> GetRestaurantByLocation(string locality)
        {
            Location location = new Location();

            //on premier la premiere localite de la liste retournee 
            location = LocationsDb.GetLocationByLocality(locality)[0];

            List<Restaurant> restaurants = new List<Restaurant>();
            restaurants = RestaurantsDb.GetRestaurantsByLocation(location.LocationId);
            return restaurants;

        }

    }
}
