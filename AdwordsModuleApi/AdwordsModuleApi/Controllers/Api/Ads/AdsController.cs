using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdwordsModuleApi.Adwords;
using AdwordsModuleApi.DbContext;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;

namespace AdwordsModuleApi.Controllers.Api.Ads
{
    [Route("api/ads")]
    public class AdsController : ApiController
    {
        private readonly ProductDbContext dbContext;

        public AdsController()
        {
            this.dbContext = new ProductDbContext();

        }

        [HttpPost]
        public IHttpActionResult CreateAds([FromBody]AdWordsContentLo adWordsContent)
        {
            var retVal = Adwords.ExpandedTextAds.CreateTextAds(new AdWordsUser(), adWordsContent);

            Adwords.Campaigns.SetCampaignStatus(new AdWordsUser(), adWordsContent.AdGroupLo.CampaignId,
                CampaignStatus.ENABLED);
            Adwords.AdGroupAdwords.SetAdGroupStatus(new AdWordsUser(), adWordsContent.AdGroupLo.adGroupId,
                AdGroupStatus.ENABLED);

            return Ok(retVal);
        }

        [HttpGet]
        public IHttpActionResult GetAd()
        {

            var result = dbContext.AdGroupHos.Include(p => p.ProductLos).ToList();

            // context.Makes.Include(m => m.Models)


            return Ok("HEJSA");
        }

    }
}
