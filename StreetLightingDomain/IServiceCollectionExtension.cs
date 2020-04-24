using Microsoft.Extensions.DependencyInjection;
using StreetLightingDal.Data;
using StreetLightingExternalDependencies.Services;

namespace StreetLightingDal
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddServicesForDomain(this IServiceCollection services)
        {
            services.AddTransient<StreetLightingDBContext>();
            // services.AddTransient<IAddressDataExternalService, PostcodesAddressData>();
            return services;
        }
    }
}
