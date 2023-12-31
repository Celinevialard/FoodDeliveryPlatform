﻿using DTO;
using System.Collections.Generic;

namespace DAL
{
	public interface ILocationsDB
	{
		Location GetLocationById(int LocationId);
		List<Location> GetLocations();
	}
}