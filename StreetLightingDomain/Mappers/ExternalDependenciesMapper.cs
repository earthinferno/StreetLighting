using AutoMapper;
using StreetLightingDomain.Models;
using StreetLightingExternalDependencies.Models;

namespace StreetLightingDomain.Mappers
{
    public class ExternalDependenciesMapper : Profile
    {
        public ExternalDependenciesMapper()
        {
            CreateMap<Addresses, AddressSearchResult>()
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(scr => scr.AddressList))
                .ReverseMap();

            CreateMap<Address, SurveyAddress>()
                .ReverseMap();
        }
    }
}
