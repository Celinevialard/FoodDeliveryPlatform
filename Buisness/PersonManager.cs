using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PersonManager
    {
        private IPersonsDB PersonDb { get; }
        public PersonManager(IConfiguration conf)
        {
            PersonDb = new PersonsDB(conf);
        }

        public Person GetPersonByLogin(string login, string password)
        {
            return PersonDb.GetPersonByLogin(login, password);
        }

        //Add
    }
}
