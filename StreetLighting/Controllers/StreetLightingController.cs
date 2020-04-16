using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        // Return the Name View
        public IActionResult Index(string prevBtn)
        {
            return View("Name");
        }

        [HttpPost]
        // Return the Name View
        public IActionResult Name(RespondentName data, string prevBtn, string nextBtn)
        {
            if (prevBtn != null)
            {
                return View("Index");
            }


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
            TempData.Keep();
            return View();
        }

        [HttpPost]
        // Return the Email Address View
        public IActionResult EmailAddress(RespondentEmailAddress data, string prevBtn, string nextBtn)
        {
            if (prevBtn != null)
            {
                var respondentName = new RespondentName
                {
                    FullName = TempData["FullName"] as string
                };
                return View("Name", respondentName);
            }

            if (nextBtn != null && ModelState.IsValid)
            {
                TempData["EmailAddress"] = data.EmailAddress;
                return View("Address");
            }

            TempData.Keep();
            return View();
        }

        [HttpPost]
        // Return the Home Address View
        public IActionResult Address(Address data, string prevBtn, string nextBtn)
        {
            if (prevBtn != null)
            {
                var respondentEmailAddress = new RespondentEmailAddress
                {
                    EmailAddress = TempData["EmailAddress"] as string
                };
                return View("EmailAddress", respondentEmailAddress);
            }

            if (nextBtn != null && ModelState.IsValid)
            {
                // TempData["Address"] = data;
                return View("CheckAnswers2", new RespondentAnswers { Name = TempData["FullName"] as string, EmailAddress = TempData["EmailAddress"] as string/*,  Address = data*/});
                // return View("CheckAnswers2");
            }

            TempData.Keep();
            return View();
        }


        [HttpPost]
        // Submit the checked answers
        public IActionResult CheckAnswers(RespondentAnswers data, string sendBtn)
        {
            if (sendBtn != null && ModelState.IsValid)
            {
                //TempData["Address"] = data.Address;
                return View("Finish");
            }

            TempData.Keep();
            return View();
        }

    }
}