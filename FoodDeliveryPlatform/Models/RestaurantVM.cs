﻿using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryPlatform.Models
{
    public class RestaurantVM
    {
		public int RestaurantId { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
        public string LocationName { get; set; }
    }
    public class RestaurantDetailsVM
    {
        public int RestaurantId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string LocationName { get; set; }
        public List<Dish> Dishes { get; set; }

        
    }
}




    
