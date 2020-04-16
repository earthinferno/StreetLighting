using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreetLighting.Controllers.Entities
{
    public class Respondent
    {
        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public Address Address { get; set; }
    }
}
