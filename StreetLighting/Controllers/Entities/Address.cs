using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreetLighting.Controllers.Entities
{
    public class Address
    {
        public int HouseNumber { get; set; }
        public string HouseName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    }
}
