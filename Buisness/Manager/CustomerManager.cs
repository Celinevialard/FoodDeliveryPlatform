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
    public class CustomerManager : ICustomerManager
    {
        private ICustomersDB CustomerDb { get; }
        private IPersonsDB PersonDb { get; }
        public CustomerManager(ICustomersDB customersDB, IPersonsDB personsDB)
        {
            CustomerDb = customersDB;
            PersonDb = personsDB;
        }

        /// <summary>
        /// Création d'un nouveau customer avec infos générales traitées dans PersonDb et specifiques au client dans CustomerDb
        /// </summary>
        /// <param name="person"></param>
        public bool AddCustomer(Person person)
        {
            if (PersonDb.CheckLoginExist(person.Login))
                return false;
            person = PersonDb.AddPerson(person);
            person.CustomerInfo.PersonId = person.PersonId;
            CustomerDb.AddCustomer(person.CustomerInfo);
            return true;
        }

        /// <summary>
        /// Mise à jour d'un customer
        /// </summary>
        /// <param name="person"></param>
        public void UpdateCustomer(Person person)
        {
            PersonDb.UpdatePerson(person);
            person.CustomerInfo.PersonId = person.PersonId;
            CustomerDb.UpdateCustomer(person.CustomerInfo);
        }
    }
}
