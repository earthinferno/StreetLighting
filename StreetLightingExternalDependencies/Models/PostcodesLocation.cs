using Newtonsoft.Json;
using System.Collections.Generic;

namespace StreetLightingExternalDependencies.Models
{
    public class PostcodesLocation
    {
        [JsonProperty("postcode")]
        public string PostCode { get; set; }

        [JsonProperty("latitude")]
        public decimal Latitude { get; set; }

        [JsonProperty("longitude")]
        public decimal Longitude { get; set; }

        [JsonProperty("addresses")]
        public IList<PostcodesAddress> Addresses { get; set; }
    }
}
