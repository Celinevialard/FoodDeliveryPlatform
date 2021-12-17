using DTO;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace FoodDeliveryPlatform.Models
{
	public class CartVM
    {
		public int RestaurantId { get; set; }
		public List<CartDetailsVM> CartDetails { get; set; }

		public string ToString() => JsonSerializer.Serialize(this);
	}
	public class CartDetailsVM
    {
		public int DishId { get; set; }

		public decimal DishPrice { get; set; }

		public int DishQuantity { get; set; }

		public string ToString() => JsonSerializer.Serialize(this);
	}
}