using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdwordsModuleApi.Adwords;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;

namespace AdwordsModuleApi.Controllers.Api.Ads
{
    [Route("api/ads")]
    public class AdsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult CreateAds([FromBody]AdWordsContent adWordsContent)
        {
            var result = Adwords.Campaigns.ActivateCampaign(adWordsContent);

            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult GetAd()
        {
            

            string[] words = new string[]
            {
                "Ostekage", "Othello", "Direktør", "vild", "test", "black friday", "øl", "malt",
                "hurlumhej", "hesteshow", "jylland", "silkeborg"
            };

            

            return Ok("HEJSA");
        }

    }
}
