using DTO;
using System.Collections.Generic;

namespace DAL
{
	public interface IRestaurantsDB
	{
		List<Restaurant> GetRestaurantsByLocation(int localiteId);
	}
}