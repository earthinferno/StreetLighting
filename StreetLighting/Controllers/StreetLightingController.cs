using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StreetLighting.Models;

namespace StreetLighting.Controllers
{
    public class StreetLightingController : Controller
    {
        public StreetLightingController()
        {
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
                var respondentName = new RespondentName { FullName = fullName };
                TempData.Keep();
                return View(respondentName);
            }
            TempData.Keep();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        // Return the Name View
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
                var respondentName = new RespondentEmailAddress { EmailAddress = emailAddress };
                TempData.Keep();
                return View(respondentName);
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
                TempData["EmailAddress"] = data.EmailAddress;
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
                var address = JsonConvert.DeserializeObject<RespondentAddress>(addressJson);
                var respondentAddress = new RespondentAddress { 
                    HouseNumber = address.HouseNumber, 
                    HouseName = address.HouseName, 
                    Street = address.Street, 
                    City = address.City, 
                    PostCode = address.PostCode
                };
                TempData.Keep();
                return View(respondentAddress);
            }

            TempData.Keep();
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
                        Brightness = data.Brightness.ToString()
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
                return View("Finish");
            }

            return View();
        }

    }
}