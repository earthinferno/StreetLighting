using AutoMapper;
using StreetLighting.Models;
using StreetLightingDal.Models;

namespace StreetLighting.Mappers
{
    public class StreetLightingDomainMapper : Profile
    {
        public StreetLightingDomainMapper()
        {
            CreateMap<SurveyDetails, RespondentAnswers>()
                .ForMember(dest => dest.Satisfied,
                opt => opt.MapFrom(src => src.Satisfied ? "yes" : "no"));

            CreateMap<RespondentAnswers, SurveyDetails>()
                .ForMember(dest => dest.Satisfied,
                opt => opt.MapFrom(src => src.Satisfied == "yes" ? true : false));

            CreateMap<SurveyAddress, RespondentAddress>()
                .ReverseMap();
        }
    }
}
