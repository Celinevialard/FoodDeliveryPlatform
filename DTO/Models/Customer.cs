using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class Customer 
	{
		public int CustomerId { get; set; }

		public string Address { get; set; }

		public int LocationId { get; set; }

		public Location Location { get; set; }

		public int PersonId { get; set; }
	}
}
