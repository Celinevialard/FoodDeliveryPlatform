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
    public class LocationManager
    {
        private LocationsDB LocationsDb { get; }
        public LocationManager(IConfiguration conf)
        {
            LocationsDb = new LocationsDB(conf);
        }

        public List<Location> GetLocations()
        {
            return LocationsDb.GetLocations();
        }
    }
}
