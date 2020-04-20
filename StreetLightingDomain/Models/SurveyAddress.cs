using System;
using System.Collections.Generic;
using System.Text;

namespace StreetLightingDomain.Models
{
    public class SurveyAddress
    {
        public int? HouseNumber { get; set; }
        public string HouseName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    }
}
