using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class OrderDetail
	{
		public int OrderDetailsId { get; set; }

		public int DishId { get; set; }

		public int Quantity { get; set; }

		public string OrderDetailsNote { get; set; }
	}
}
