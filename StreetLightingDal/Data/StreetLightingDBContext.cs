using Microsoft.EntityFrameworkCore;
using StreetLightingDal.Models;

namespace StreetLightingDal.Data
{
    public class StreetLightingDBContext : DbContext, IStreetLightingDBContext
    {
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Respondent> Respondent { get; set; }
        public virtual  DbSet<Response> QuestionnaireResponse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
                   => options.UseSqlite("Data Source=streetLighting.db");
    }
}

//(to run migrations in console from startup project - StreetLighting):
//dotnet ef migrations add initialCreate --output-dir Data/Migrations
//dotnet ef database update -p ..\StreetLightingDal\StreetLightingDal.csproj -s .\StreetLighting.csproj
//to remove:
//dotnet ef database update initial (or 'dotnet ef database update 0' to remove all)
//and
//dotnet ef migrations remove
