using Microsoft.EntityFrameworkCore;
using StreetLightingDal.Models;

namespace StreetLightingDal.Data
{
    public interface IStreetLightingDBContext
    {
        DbSet<Address> Address { get; set; }
        DbSet<Response> QuestionnaireResponse { get; set; }
        DbSet<Respondent> Respondent { get; set; }
    }
}