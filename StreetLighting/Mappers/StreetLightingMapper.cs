using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using StreetLighting.Models;
using StreetLightingDomain.Models;

namespace StreetLighting.Mappers
{
    public class StreetLightingMapper : Profile
    {
        public StreetLightingMapper()
        {
            CreateMap<SurveyDetails, RespondentAnswers>()
                .ForMember(dest => dest.Satisfied,
                opt => opt.MapFrom(src => src.Satisfied ? "yes" : "no"));

            CreateMap<RespondentAnswers, SurveyDetails>()
                .ForMember(dest => dest.Satisfied,
                opt => opt.MapFrom(src => src.Satisfied == "yes" ? true : false));

            CreateMap<SurveyAddress, RespondentAddress>()
                .ReverseMap();

            CreateMap<AddressSearchResult, PostcodeAddresses>();

            CreateMap<SurveyAddress, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.AddressLine1))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => $"{src.AddressLine1} {src.AddressLine2} {src.City} {src.PostCode}"));
        }
    }
}
