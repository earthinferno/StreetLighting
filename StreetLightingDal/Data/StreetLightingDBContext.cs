using Microsoft.EntityFrameworkCore;
using StreetLightingDal.Models;

namespace StreetLightingDal.Data
{
    public class StreetLightingDBContext : DbContext, IStreetLightingDBContext
    {


        public DbSet<Address> Address { get; set; }
        public DbSet<Respondent> Respondent { get; set; }
        public DbSet<Response> QuestionnaireResponse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
                   => options.UseSqlite("Data Source=streetLighting.db");
    }
}

//(to run migrations in console):
//dotnet ef migrations add initialCreate --output-dir Data/Migrations
//dotnet ef database update
//to remove:
//dotnet ef database update initial (or 'dotnet ef database update 0' to remove all)
//and
//dotnet ef migrations remove
