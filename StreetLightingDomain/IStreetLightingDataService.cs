using StreetLightingDomain.Models;

namespace StreetLightingDomain
{
    public interface IStreetLightingDataService
    {
        void SaveSurveyResponse(SurveyDetails survey);
    }
}