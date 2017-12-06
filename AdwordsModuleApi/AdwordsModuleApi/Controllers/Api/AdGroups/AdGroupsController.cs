using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201708;
using AdGroupStatus = Google.Api.Ads.AdWords.v201710.AdGroupStatus;

namespace AdwordsModuleApi.Controllers.Api.AdGroups
{
    [Route("api/adgroups")]
    public class AdGroupsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult CreateAdGroup([FromBody] AdGroupLo adGroupLo)
        {
            var retVal = Adwords.AdGroupAdwords.CreateAdGroup(new AdWordsUser(), adGroupLo);

            string[] keyWords = adGroupLo.KeyWords.Split(',');

            var retValKeyWords = Adwords.AdwordsKeyword.AdKeyWordsToAdGroup(new AdWordsUser(), retVal.value[0].id, keyWords);

            return Ok(retVal);
        }

        [Route("api/adgroups/{id:long}")]
        [HttpGet]
        public IHttpActionResult GetAdGroups(long id)
        {
            var adGroups = Adwords.AdGroupAdwords.GetAdGroups(new AdWordsUser(), id);

            return Ok(adGroups);
        }

        [Route("api/adgroups/delete/{id:long}")]
        [HttpDelete]
        public IHttpActionResult DeleteAdGroup(long id)
        {
            var adGroup = Adwords.AdGroupAdwords.SetAdGroupStatus(new AdWordsUser(), id, AdGroupStatus.REMOVED);

            return Ok(adGroup);
        }
    }
}
