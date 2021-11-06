using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface IDishManager
    {
        List<Dish> GetDishesByRestaurant(int RestaurantId);
    }
}