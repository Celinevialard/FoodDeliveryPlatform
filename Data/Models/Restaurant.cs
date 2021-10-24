﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class Restaurant
	{
		public int RestaurantId { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
		public int LocationId { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
