using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class Person
	{
		public string FirstName { get; set; }

		public string Name { get; set; }

		public Customer CustomerInfo { get; set; }

		public Courrier CourrierInfo { get; set; }
	}
}
