using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IRestaurantManager
    {
        List<Restaurant> GetRestaurantByLocation(string locality);
        List<Restaurant> GetRestaurantByLocation(int locationId);

        Restaurant GetRestaurantById(int idRestaurant);
    }
}