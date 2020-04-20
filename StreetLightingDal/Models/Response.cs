using System;
using System.Collections.Generic;
using System.Text;

namespace StreetLightingDal.Models
{
    public class Response
    {
        public int ResponseId { get; set; }
        public bool Satisfied { get; set; }
        public int BrightnessLevel { get; set; }
        
        public int RespondentId { get; set; }
        public virtual Respondent Respondent { get; set; }
    }
}
