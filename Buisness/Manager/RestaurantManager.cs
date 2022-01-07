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



        public List<Restaurant> GetRestaurantByLocation(int locationId)
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            List<int> locationsId = new List<int>();
            List<int> locationsIdAlreadyRead = new List<int>();
            List<int> courriersId = new List<int>();
            courriersId = CourriersDb.GetCourriersIdByLocationId(locationId);

            //BUG si habite une région non livrable
            foreach (int courrierId in courriersId)
            {
                List<int> locationsTemp = CourriersDb.GetLocationsByCourrierId(courrierId);
                if (locationsTemp != null)
                    locationsId.AddRange(locationsTemp);
            }


            //DONE
            //Etape1 : obtenir la liste des livreurs pour un locationId à créer
            //Etapre2: obtenir la liste des localités pour chaque livreur
            //boucler sur la liste des localités .addRange


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


            if (restaurants == null)
                return null;

            foreach (Restaurant restaurant in restaurants)
            {
                restaurant.Location = LocationsDb.GetLocationById(restaurant.LocationId);
            }

            return restaurants;
        }


        public Restaurant GetRestaurantById(int idRestaurant)
        {
            return RestaurantsDb.GetRestaurantsById(idRestaurant);
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
