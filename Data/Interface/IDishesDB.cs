using DTO;
using System.Collections.Generic;

namespace DAL
{
	public interface IDishesDB
	{
		List<Dish> GetDishesByRestaurantId(int restaurantId);
	}
}