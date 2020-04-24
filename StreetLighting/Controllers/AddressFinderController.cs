using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StreetLighting.Models;
using StreetLightingDomain;
using StreetLightingDomain.Models;

namespace StreetLighting.Controllers
{
    public class AddressFinderController : Controller
    {
        private readonly IAddressLookUpService _addressLookUpService;
        private readonly IMapper _mapper;

        public AddressFinderController(
            IAddressLookUpService addressLookUpService,
            IMapper mapper)
        {
            _addressLookUpService = addressLookUpService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var postcode = TempData["Postcode"] as string;
            if (!string.IsNullOrWhiteSpace(postcode))
            {
                TempData.Keep();
                return View(new RespondentPostcode { Postcode = postcode });
            }

            TempData.Keep();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressLookup(RespondentPostcode data, string findAddress)
        {
            if (findAddress != null && ModelState.IsValid)
            {
                //var addressSearchResults = await _addressLookUpService.GetAddressByPostCode(data.Postcode);
                //var addressJson = JsonConvert.SerializeObject(addressSearchResults);
                var addressSearchResults = JsonConvert.DeserializeObject<AddressSearchResult>(
                    "{\"Addresses\":[{\"AddressLine1\":\"1 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"10 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"12 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"14 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"16 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"18 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"2 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"20 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"22 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"23 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"24 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"25 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"27 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"29 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"3 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"31 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"4 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"5 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"6 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"7 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"8 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"9 Hobben Crescent\",\"AddressLine2\":\"\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"},{\"AddressLine1\":\"Brooks Consulting Group Ltd\",\"AddressLine2\":\"7 Hobben Crescent\",\"City\":\"Nottingham\",\"PostCode\":\"NG158HN\"}]}");

                var postcodeAddresses = _mapper.Map<PostcodeAddresses>(addressSearchResults);
                if (postcodeAddresses.Addresses?.Count > 0)
                {
                    TempData["addressSearchResults"] = JsonConvert.SerializeObject(addressSearchResults);
                    return View("AddressSelection", postcodeAddresses);
                }
            }

            TempData.Keep();
            return View();
        }

        //public IActionResult AddressSelection(PostcodeAddresses postcodeAddresses)
        //{
        //    if (postcodeAddresses?.Addresses.Count > 0)
        //    {
        //        var postcode = TempData["Postcode"] as string;
        //        TempData.Keep();
        //        return View(new RespondentPostcode { Postcode = postcode });
        //    }

        //    TempData.Keep();
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddressSelection(PostcodeAddresses postcodeAddresses, string nextBtn)
        {
            if (!string.IsNullOrWhiteSpace(postcodeAddresses?.SelectedAddress) && ModelState.IsValid)
            {
                var addressSearchResults = JsonConvert.DeserializeObject<AddressSearchResult>(TempData["addressSearchResults"] as string);
                var surveyAddress = addressSearchResults.Addresses.Where(address => address.AddressLine1 == postcodeAddresses.SelectedAddress).FirstOrDefault();
                var respondentAddress = _mapper.Map<RespondentAddress>(surveyAddress);

                if (respondentAddress != null)
                {
                    TempData["Address"] = JsonConvert.SerializeObject(respondentAddress);
                    TempData.Keep();
                    return Redirect("/StreetLighting/Lighting");
                }
            }

            TempData.Keep();
            return View();
        }
    }
}