using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class Order
	{
		public int OrderId { get; set; }

		public int CustomerId { get; set; }

		public int CourrierId { get; set; }

		public int RestaurantId { get; set; }

		public string Address { get; set; }

		public int LocationId { get; set; }

		public string OrderNote { get; set; } = string.Empty;

		public DateTime OrderDate { get; set; }

		public decimal TotalAmount { get; set; }

		public OrderStatusEnum Status { get; set; } 

		public List<OrderDetail> Details { get; set; }
	}
}
