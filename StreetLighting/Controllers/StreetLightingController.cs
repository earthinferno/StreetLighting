using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StreetLighting.Models;
using StreetLightingDal;
using StreetLightingDal.Models;
using StreetLightingDomain;
using System;

namespace StreetLighting.Controllers
{
    public class StreetLightingController : Controller
    {
        private readonly IStreetLightingDataService _streetLightingDataService;
        private readonly IAddressLookUpService _addressLookUpService;
        private readonly IMapper _mapper;

        public StreetLightingController(
            IStreetLightingDataService streetLightingDataService,
            IAddressLookUpService addressLookUpService,
            IMapper mapper
        )
        {
            _streetLightingDataService = streetLightingDataService;
            _addressLookUpService = addressLookUpService;
            _mapper = mapper;
        }
        // Return the index view
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string prevBtn)
        {
            TempData.Remove("FullName");
            TempData.Remove("EmailAddress");
            TempData.Remove("Address");
            TempData.Remove("Satisfied");
            TempData.Remove("Brightness");
            TempData.Remove("Lighting");
            return View("Name");
        }


        public IActionResult Name()
        {
            var fullName = TempData["FullName"] as string;
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                TempData.Keep();
                return View(new RespondentName { FullName = fullName });
            }
            TempData.Keep();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Name(RespondentName data, string nextBtn)
        {
            if (nextBtn != null && ModelState.IsValid)
            {
                TempData["FullName"] = data.FullName;
                return Redirect("EmailAddress");
            }

            TempData.Keep();
            return View();
        }


        public IActionResult EmailAddress()
        {

            var emailAddress = TempData["EmailAddress"] as string;
            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                TempData.Keep();
                return View(new RespondentEmailAddress { Email = emailAddress });
            }

            TempData.Keep();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EmailAddress(RespondentEmailAddress data, string nextBtn)
        {
            if (nextBtn != null && ModelState.IsValid)
            {
                TempData["EmailAddress"] = data.Email;
                return Redirect("Address");
            }

            TempData.Keep();
            return View();
        }


        public IActionResult Address()
        {
            var addressJson = TempData["Address"] as string;
            if (!string.IsNullOrWhiteSpace(addressJson))
            {
                TempData.Keep();
                return View(JsonConvert.DeserializeObject<RespondentAddress>(addressJson));
            }

            TempData.Keep();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Address(string postCode, string nextBtn)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Address(RespondentAddress data, string nextBtn)
        {
            if (nextBtn != null && ModelState.IsValid)
            {
                TempData["Address"] = JsonConvert.SerializeObject(data);
                TempData.Keep();
                return Redirect("Lighting");
            }

            TempData.Keep();
            return View();
        }

        public IActionResult Lighting()
        {
            var satisfied = TempData["Satisfied"] as string;
            if (!string.IsNullOrWhiteSpace(satisfied))
            {
                var respondentAddress = new LightingResponse
                {
                    Satisfied = satisfied == "yes" ? true : false
                };
                TempData.Keep();
                return View(respondentAddress);
            }

            TempData.Keep();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Lighting(LightingResponse data, string nextBtn)
        {

            if (nextBtn != null && ModelState.IsValid && data.Satisfied.HasValue)
            {
                TempData["Satisfied"] = data.Satisfied == true ? "yes" : "no" ;
                TempData.Keep();
                return Redirect("Brightness");
            }

            TempData.Keep();
            return View();

        }

        public IActionResult Brightness()
        {
            var brightness = int.TryParse(TempData["Brightness"] as string, out var tempval) ? tempval : (int?)null;
            if (brightness != null)
            {
                var respondentAddress = new BrightnessResponse
                {
                    Brightness = brightness.GetValueOrDefault()
                };
                TempData.Keep();
                return View(respondentAddress);
            }

            TempData.Keep();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Brightness(BrightnessResponse data, string nextBtn)
        {
            if (nextBtn != null && ModelState.IsValid)
            {
                TempData["Brightness"] = data.Brightness.ToString();
                TempData.Keep();

                return View("CheckAnswers",
                    new RespondentAnswers
                    {
                        Name = TempData["FullName"] as string,
                        EmailAddress = TempData["EmailAddress"] as string,
                        Address = JsonConvert.DeserializeObject<RespondentAddress>(TempData["Address"] as string),
                        Satisfied = TempData["Satisfied"] as string,
                        Brightness = data.Brightness
                    });
            }

            TempData.Keep();
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckAnswers(RespondentAnswers data, string sendBtn)
        {
            if (sendBtn != null && ModelState.IsValid)
            {
                var surveyDetails = _mapper.Map<SurveyDetails>(data);
                try
                {
                    _streetLightingDataService.SaveSurveyResponse(surveyDetails);
                }
                catch (Exception ex)
                {
                    // refactor - something better than this.
                    return Redirect("/Home/Error");
                }
                
                return View("Finish");
            }

            return View();
        }

    }
}