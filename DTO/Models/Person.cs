using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class Person
	{
		public int PersonId { get; set; }

		public string FirstName { get; set; }

		public string Name { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public Customer CustomerInfo { get; set; }

		public Courrier CourrierInfo { get; set; }
	}
}
