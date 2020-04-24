using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StreetLighting.Models
{
    public class PostcodeAddresses
    {
        //public List<RespondentAddress> AddressList { get; set; }
        public List<SelectListItem> Addresses { get; set;  }


        [Required]
        public string SelectedAddress { get; set; }
    }
}
