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
    public class PersonManager : IPersonManager
    {
        private IPersonsDB PersonDb { get; }
        public PersonManager(IPersonsDB personsDB)
        {
            PersonDb = personsDB;
        }
        /// <summary>
        /// Obtenir le profil du client/livreur
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Person GetPersonByLogin(string login, string password)
        {
            return PersonDb.GetPersonByLogin(login, password);
        }


    }
}
