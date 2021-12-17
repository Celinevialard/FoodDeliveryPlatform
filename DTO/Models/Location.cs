using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Location
    {
        public int LocationId { get; set; }
        public string NPA { get; set; }
        public string Locality { get; set; }

        public string LocationName { get => $"{NPA} {Locality}"; }
        public override string ToString()
        {
            return Locality;
        }
    }
}
