using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;

namespace AdwordsModuleApi.Controllers.Api.Ads
{
    [Route("api/ads")]
    public class AdsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult CreateAds()
        {
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetAd()
        {
            Adwords.Keyword.GetKeyWords(
                "Engelsk inspireret Porter, der er brygget på 5 forskellige malttyper, hvor nogle af dem er røgmalt, karamelmalt og chokolademalt");

            

            //ActivateCampaign activate = new ActivateCampaign()
            //{
            //    CampaignId = 980217086,
            //    ExpandedTextAd =
            //    {
            //        new ExpandedTextAdDto()
            //        {
            //            HeadLinePart1 = "Test HeadLine1",
            //            HeadLinePart2 = "Test HeadLine2",
            //            Description =  "Endnu et test produkt",
            //            FinalUrls = new []{ "http://nolleren.org/test" },
            //            ProductName = "Test Produkt",
            //            ProductNumber = "12345"
            //        }
            //    }
            //};

            //Adwords.Campaigns.ActivateCampaign(activate);

            return Ok();
        }
    }
}
