using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryPlatform.Models
{

    public class DishVM
    {
        public int DishId { get; set; }

        public int RestaurantId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Allergies { get; set; }

        public decimal Price { get; set; }

        public string ImageLink { get; set; }
    }
}




    
