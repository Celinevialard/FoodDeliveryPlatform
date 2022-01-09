using DTO;
using System.Collections.Generic;

namespace DAL
{
	public interface ICourriersDB
	{
		List<int> GetLocationsByCourrierId(int id);
		List<Courrier> GetCourrierByLocalite(int depart, int arriver);
		List<int> GetCourriersIdByLocationId(int locationId);
	}
}