﻿using DAL;
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

        private ILocationsDB LocationsDb { get; }
        public PersonManager(IPersonsDB personsDB, ILocationsDB locationsDb)
        {
            PersonDb = personsDB;
            LocationsDb = locationsDb;
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

        /// <summary>
        /// Recupère une personne en fonction du customer id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Person GetPersonByCustomer(int customerId)
        {
            Person person = PersonDb.GetPersonByCustomer(customerId);
            person.CustomerInfo.Location = LocationsDb.GetLocationById(person.CustomerInfo.LocationId);
            return person;
        }

        /// <summary>
        /// Récupère une personne en fonction du courrier id
        /// </summary>
        /// <param name="courrierId"></param>
        /// <returns></returns>
        public Person GetPersonByCourrier(int courrierId)
        {
            Person person = PersonDb.GetPersonByCourrier(courrierId);
            return person;
        }
    }
}
