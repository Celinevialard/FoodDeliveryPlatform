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
    public class CourrierManager : ICourrierManager
    {
        private ICourriersDB CourrierDb { get; }

        public CourrierManager(ICourriersDB courriersDB)
        {
            CourrierDb = courriersDB;
        }
        /// <summary>
        /// Création d'un nouveau livreur étant déjà dans la base de person
        /// </summary>
        /// <param name="courrier"></param>
        public void AddCourrier(Courrier courrier)
        {
            CourrierDb.AddCourrier(courrier);
        }



        /// <summary>
        /// Obtenir les zones de livraison d'un livreur
        /// </summary>
        /// <param name="courrierId"></param>
        /// <returns>Liste des localites</returns>
        public List<int> GetLocationsByCourrierId(int courrierId)
        {
            List<int> locations = CourrierDb.GetLocationsByCourrierId(courrierId);
            return locations;
        }



    }
}
