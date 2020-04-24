using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StreetLighting.Models
{
    public class RespondentPostcode
    {
        [Required]
        [RegularExpression(@"^[A-Za-z]{1,2}[0-9\s]{2,4}[A-Za-z]{2,3}$", ErrorMessage = "Invalid Postcode")]
        public string Postcode { get; set; }
    }
}
