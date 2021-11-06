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
    public class CourrierManager
    {
        private ICourriersDB CourrierDb { get; }

        public CourrierManager(IConfiguration conf)
        {
            CourrierDb = new CourriersDB(conf);
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
        /// <param name="courrier"></param>
        /// <returns></returns>
        public List<int> GetDeliveryZoneByCourrier(Courrier courrier)
        {
            List<int> deliveryZones = CourrierDb.GetDeliveryZoneByCourrierId(courrier.CourrierId);
            return deliveryZones;
        }



    }
}
