using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace FoodDeliveryPlatform.Models
{
	public class CartVM
    {
		public int RestaurantId { get; set; }
		public List<CartDetailsVM> CartDetails { get; set; }
		public DateTime DateDelivery { get; set; }
		public List<DateTime> DatesDelivery { get; set; }

		[StringLength(250)]
		public string OrderNote { get; set; } = string.Empty;

        public string ToString() => JsonSerializer.Serialize(this);
	}
	public class CartDetailsVM
    {
		public int DishId { get; set; }

		public string DishName { get; set; }

		public decimal DishPrice { get; set; }

		public int DishQuantity { get; set; }

		[StringLength(250)]
		public string Note { get; set; } = string.Empty;

		public string ToString() => JsonSerializer.Serialize(this);
	}
}