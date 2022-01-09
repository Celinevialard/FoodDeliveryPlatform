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
        private ICourriersDB CourriersDb { get; }
        public RestaurantManager(IRestaurantsDB restaurantsDB, ILocationsDB locationsDB, ICourriersDB courriersDB)
        {
            RestaurantsDb = restaurantsDB;
            LocationsDb = locationsDB;
            CourriersDb = courriersDB;
        }

        /// <summary>
        /// Récupère une liste de restaurant en fonction d'une localité
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public List<Restaurant> GetRestaurantByLocation(int locationId)
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            List<int> locationsId = new List<int>();
            List<int> locationsIdAlreadyRead = new List<int>();
            List<int> courriersId = new List<int>();
            courriersId = CourriersDb.GetCourriersIdByLocationId(locationId);

            if (courriersId == null)
                return null;

            foreach (int courrierId in courriersId)
            {
                List<int> locationsTemp = CourriersDb.GetLocationsByCourrierId(courrierId);
                if (locationsTemp != null)
                    locationsId.AddRange(locationsTemp);
            }

            foreach (int location in locationsId)
            {
                if (!locationsIdAlreadyRead.Contains(location))
                {
                    List<Restaurant> restautant = RestaurantsDb.GetRestaurantsByLocation(location);
                    if (restautant != null)
                        restaurants.AddRange(RestaurantsDb.GetRestaurantsByLocation(location));
                    locationsIdAlreadyRead.Add(location);
                }
            }

            if (restaurants == null )
                return null;

            foreach (Restaurant restaurant in restaurants)
            {
                restaurant.Location = LocationsDb.GetLocationById(restaurant.LocationId);
            }

            return restaurants;
        }

        /// <summary>
        /// Récupère un restaurant par son id
        /// </summary>
        /// <param name="idRestaurant"></param>
        /// <returns></returns>
        public Restaurant GetRestaurantById(int idRestaurant)
        {
            Restaurant restaurant = RestaurantsDb.GetRestaurantsById(idRestaurant);
            if (restaurant == null)
                return null;
            restaurant.Location = LocationsDb.GetLocationById(restaurant.LocationId);
            return restaurant;
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
