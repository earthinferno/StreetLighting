using AutoMapper;
using StreetLightingExternalDependencies.Models;

namespace StreetLightingExternalDependencies.Mappers
{
    public class AddressDataExternalServiceMapper : Profile
    {
        public AddressDataExternalServiceMapper()
        {
            #region works
            CreateMap<PostcodesLocation, Addresses>()
                .ForMember(dest => dest.AddressList, opt => opt.MapFrom(src => src.Addresses));

            CreateMap<PostcodesAddress, Address>()
            .ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => src.Line1))
            .ForMember(dest => dest.AddressLine2, opt => opt.MapFrom(src => src.Line2))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.TownOrCity));
            #endregion
        }
    }
}
