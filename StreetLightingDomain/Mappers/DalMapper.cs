using AutoMapper;
using StreetLightingDal.Models;
using StreetLightingDomain.Models;

namespace StreetLightingDomain.Mappers
{
    public class DalMapper : Profile
    {
        public DalMapper()
        {
            CreateMap<Respondent, SurveyDetails>()
                .ForMember(dest => dest.Brightness,
                opt => opt.MapFrom(src => src.QuestionnaireResponse.BrightnessLevel))
                .ForMember(dest => dest.Satisfied,
                opt => opt.MapFrom(src => src.QuestionnaireResponse.Satisfied))
                .ReverseMap();

            CreateMap<Address, SurveyAddress>()
                .ReverseMap();
        }
    }
}
