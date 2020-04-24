using StreetLightingDomain.Models;

namespace StreetLightingDal
{
    public interface IStreetLightingDataService
    {
        void SaveSurveyResponse(SurveyDetails survey);
    }
}