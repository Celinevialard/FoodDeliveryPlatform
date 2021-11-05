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
    public class CustomerManager
    {
        private ICustomersDB CustomerDb { get; }
        private IPersonsDB PersonDb { get; }
        public CustomerManager(IConfiguration conf)
        {
            CustomerDb = new CustomersDB(conf);
            PersonDb = new PersonsDB(conf);
        }

        public void AddCustomer(Person person)
        {
            PersonDb.AddPerson(person);
            CustomerDb.AddCustomer(person.CustomerInfo);
        }
    }
}
