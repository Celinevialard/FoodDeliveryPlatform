using DTO;
using System.Collections.Generic;

namespace DAL
{
	public interface ICourriersDB
	{
		Courrier AddCourrier(Courrier courrier);
		List<int> GetLocationsByCourrierId(int id);
		List<Courrier> GetCourrierByLocalite(int depart, int arriver);
		List<int> GetCourriersIdByLocationId(int locationId);
	}
}