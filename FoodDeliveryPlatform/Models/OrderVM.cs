using DTO;
using System;
using System.Collections.Generic;

namespace FoodDeliveryPlatform.Models
{
    public class OrderVM
    {
		public int OrderId { get; set; }

		public string CustomerName { get; set; }

		public string CustomerAddress { get; internal set; }

		public string OrderNote { get; set; } = string.Empty;

		public DateTime OrderDate { get; set; }

		public decimal TotalAmount { get; set; }

		public OrderStatusEnum Status { get; set; }

		public List<OrderDetailVM> Details { get; set; }
        public string CustomerLocation { get; internal set; }
    }

	public class OrderDetailVM
    {
		public string DishName { get; set; }

		public int Quantity { get; set; }

		public string OrderDetailsNote { get; set; } = string.Empty;
	}
}