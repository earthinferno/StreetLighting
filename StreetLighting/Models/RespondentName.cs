﻿using System.ComponentModel.DataAnnotations;

namespace StreetLighting.Models
{
    public class RespondentName
    {
        [Required]
        public string FullName { get; set; }
    }
}
