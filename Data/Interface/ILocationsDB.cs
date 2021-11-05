using DTO;
using System.Collections.Generic;

namespace DAL
{
	public interface ILocationsDB
	{
		Location GetLocationById(int LocationId);
		List<Location> GetLocationByLocality(string Locality);
		List<Location> GetLocationByNPA(string NPA);
		List<Location> GetLocations();
	}
}