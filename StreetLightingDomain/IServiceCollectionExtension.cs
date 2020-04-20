using Microsoft.Extensions.DependencyInjection;
using StreetLightingDal.Data;

namespace StreetLightingDomain
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddSQLiteDatabaseConnector(this IServiceCollection services)
        {
            services.AddTransient<StreetLightingDBContext>();
            return services;
        }
    }
}
