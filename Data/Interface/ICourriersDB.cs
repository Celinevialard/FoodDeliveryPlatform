using DTO;
using System.Collections.Generic;

namespace DAL
{
	public interface ICourriersDB
	{
		Courrier AddCourrier(Courrier courrier);
		List<int> GetDeliveryZoneByCourrierId(int id);
	}
}