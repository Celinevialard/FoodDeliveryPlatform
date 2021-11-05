using DTO;
using System.Collections.Generic;

namespace DAL
{
	public interface IPersonsDB
	{
		Person AddPerson(Person person);
		List<Person> GetPersonByLogin(string login, string password);
	}
}