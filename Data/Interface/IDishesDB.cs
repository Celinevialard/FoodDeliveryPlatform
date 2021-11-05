using DTO;
using System.Collections.Generic;

namespace DAL
{
    public interface IDishesDB
    {
        Dish GetDishById(int Dishid);
        List<Dish> GetDishesByRestaurantId(int restaurantId);
    }
}