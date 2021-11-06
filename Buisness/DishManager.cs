using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DishManager
    {
        private IDishesDB DishesDb { get; }
        public DishManager(IConfiguration conf)
        {
            DishesDb = new DishesDB(conf);
        }
        public List<Dish> GetDishesByRestaurant(int RestaurantId)
        {
            return DishesDb.GetDishesByRestaurantId(RestaurantId);
        }
    }
}
