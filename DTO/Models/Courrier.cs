using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class Courrier 
	{
		public int CourrierId { get; set; }

		public int PersonId { get; set; }

		public List<int> LocationsId { get; set; }

		public List<Order> Orders { get; set; }

	}
}
