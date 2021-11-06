using DTO;
using System.Collections.Generic;

namespace BLL
{
    public interface ILocationManager
    {
        List<Location> GetLocations();
    }
}