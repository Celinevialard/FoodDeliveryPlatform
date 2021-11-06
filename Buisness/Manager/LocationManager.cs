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
    public class LocationManager : ILocationManager
    {
        private ILocationsDB LocationsDb { get; }
        public LocationManager(IConfiguration conf)
        {
            LocationsDb = new LocationsDB(conf);
        }

        /// <summary>
        /// Liste toutes les localités
        /// </summary>
        /// <returns></returns>
        public List<Location> GetLocations()
        {
            return LocationsDb.GetLocations();
        }
    }
}
