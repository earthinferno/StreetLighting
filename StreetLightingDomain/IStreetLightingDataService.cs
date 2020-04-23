using StreetLightingDal.Models;

namespace StreetLightingDal
{
    public interface IStreetLightingDataService
    {
        void SaveSurveyResponse(SurveyDetails survey);
    }
}