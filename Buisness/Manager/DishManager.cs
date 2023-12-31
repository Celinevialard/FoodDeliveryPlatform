﻿using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DishManager : IDishManager
    {
        private IDishesDB DishesDb { get; }
        public DishManager(IDishesDB dishesDB)
        {
            DishesDb = dishesDB;
        }

        /// <summary>
        /// Liste les plats pour un restaurant 
        /// </summary>
        /// <param name="RestaurantId"></param>
        /// <returns></returns>
        public List<Dish> GetDishesByRestaurant(int RestaurantId)
        {
            return DishesDb.GetDishesByRestaurantId(RestaurantId);
        }

        /// <summary>
        /// Recuper un plat en fonction de son id
        /// </summary>
        /// <param name="dishId"></param>
        /// <returns></returns>
        public Dish GetDish(int dishId)
        {
            return DishesDb.GetDishById(dishId);
        }
    }
}


