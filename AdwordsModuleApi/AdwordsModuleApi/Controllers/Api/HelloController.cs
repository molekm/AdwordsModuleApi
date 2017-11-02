using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;

namespace AdwordsModuleApi.Controllers.Api
{
    [Route("api/hello")]
    public class HelloController : ApiController
    {
        [HttpGet]
        public IHttpActionResult SayHello()
        {
            CampaignPage page = new CampaignPage();

            page = Run(new AdWordsUser());

            return Ok(page.entries);
        }

        public CampaignPage Run(AdWordsUser user)
        {
            using (CampaignService campaignService =
                (CampaignService)user.GetService(AdWordsService.v201710.CampaignService))
            {

                // Create the selector.
                Selector selector = new Selector()
                {
                    fields = new string[] {
                 Campaign.Fields.Id, Campaign.Fields.Name, Campaign.Fields.Labels, Campaign.Fields.Status
                 //},
                 //   predicates = new Predicate[] {
          // Labels filtering is performed by ID. You can use CONTAINS_ANY to
          // select campaigns with any of the label IDs, CONTAINS_ALL to select
          // campaigns with all of the label IDs, or CONTAINS_NONE to select
          // campaigns with none of the label IDs.
          //Predicate.ContainsAny(Campaign.Fields.Labels, new string[] { labelId.ToString() })
        },
                    paging = Paging.Default
                };

                CampaignPage page = new CampaignPage();

                try
                {
                    // Get the campaigns.
                    page = campaignService.get(selector);
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to retrieve campaigns by label", e);
                }
                return page;
            }
        }
    }
}


    public class Person
    {
        public string name;
        public string address;
        public int age;
    }

