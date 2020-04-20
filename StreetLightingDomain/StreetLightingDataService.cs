using StreetLightingDomain.Models;
using StreetLightingDal.Data;
using StreetLightingDal.Models;
using System;
using AutoMapper;

namespace StreetLightingDomain
{
    public class StreetLightingDataService : IStreetLightingDataService
    {
        private readonly IMapper _mapper;
        private readonly StreetLightingDBContext _dbContext;
        public StreetLightingDataService(StreetLightingDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }
        public void SaveSurveyResponse(SurveyDetails survey)
        {
            var respondent = _mapper.Map<Respondent>(survey);
            _dbContext.Respondent.Add(respondent);
            _dbContext.SaveChanges();
        }
    }
}
