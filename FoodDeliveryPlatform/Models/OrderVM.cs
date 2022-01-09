using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryPlatform.Models
{
	public class OrderIndexVM
	{
		public List<OrderVM> OrdersCustomer { get; set; }
		public List<OrderVM> OrdersDelivery { get; set; }
	}
	public class OrderVM
    {
		public int OrderId { get; set; }

		public string CustomerName { get; set; }

		public string CustomerAddress { get; set; }

		public string OrderNote { get; set; } = string.Empty;

		public DateTime OrderDate { get; set; }

		public decimal TotalAmount { get; set; }

		public OrderStatusEnum Status { get; set; }

		public List<OrderDetailVM> Details { get; set; }
        public string CustomerLocation { get; set; }
        public string CourrierName { get; set; }

		public bool ForCourrier { get; set; }

	}

	public class OrderDetailVM
    {
		public string DishName { get; set; }

		public int Quantity { get; set; }

		public string OrderDetailsNote { get; set; } = string.Empty;
	}


}