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

            Run(new AdWordsUser());

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
        void Run(AdWordsUser user)
        {
            using (TrafficEstimatorService trafficEstimatorService =
                (TrafficEstimatorService)user.GetService(
                    AdWordsService.v201710.TrafficEstimatorService))
            {

                // Create keywords. Refer to the TrafficEstimatorService documentation for the maximum
                // number of keywords that can be passed in a single request.
                //   https://developers.google.com/adwords/api/docs/reference/latest/TrafficEstimatorService
                Keyword keyword1 = new Keyword();
                keyword1.text = "ostekage";
                keyword1.matchType = KeywordMatchType.EXACT;

                Keyword keyword2 = new Keyword();
                keyword2.text = "malt";
                keyword2.matchType = KeywordMatchType.EXACT;

                Keyword keyword3 = new Keyword();
                keyword3.text = "og";
                keyword3.matchType = KeywordMatchType.EXACT;

                Keyword[] keywords = new Keyword[] { keyword1, keyword2, keyword3 };

                // Create a keyword estimate request for each keyword.
                List<KeywordEstimateRequest> keywordEstimateRequests = new List<KeywordEstimateRequest>();

                foreach (Keyword keyword in keywords)
                {
                    KeywordEstimateRequest keywordEstimateRequest = new KeywordEstimateRequest();
                    keywordEstimateRequest.keyword = keyword;
                    keywordEstimateRequests.Add(keywordEstimateRequest);
                }

                // Create ad group estimate requests.
                AdGroupEstimateRequest adGroupEstimateRequest = new AdGroupEstimateRequest();
                adGroupEstimateRequest.keywordEstimateRequests = keywordEstimateRequests.ToArray();
                adGroupEstimateRequest.maxCpc = new Money();
                adGroupEstimateRequest.maxCpc.microAmount = 1000000;

                // Create campaign estimate requests.
                CampaignEstimateRequest campaignEstimateRequest = new CampaignEstimateRequest();
                campaignEstimateRequest.adGroupEstimateRequests = new AdGroupEstimateRequest[] {
            adGroupEstimateRequest};

                // Optional: Set additional criteria for filtering estimates.
                // See http://code.google.com/apis/adwords/docs/appendix/countrycodes.html
                // for a detailed list of country codes.
                Location countryCriterion = new Location();
                countryCriterion.id = 2208; //US 1005310

                // See http://code.google.com/apis/adwords/docs/appendix/languagecodes.html
                // for a detailed list of language codes.
                Language languageCriterion = new Language();
                languageCriterion.id = 1009; //en

                campaignEstimateRequest.criteria = new Criterion[] { countryCriterion, languageCriterion };

                try
                {
                    // Create the selector.
                    TrafficEstimatorSelector selector = new TrafficEstimatorSelector()
                    {
                        campaignEstimateRequests = new CampaignEstimateRequest[] { campaignEstimateRequest },

                        // Optional: Request a list of campaign level estimates segmented by platform.
                        platformEstimateRequested = true
                    };

                    // Get traffic estimates.
                    TrafficEstimatorResult result = trafficEstimatorService.get(selector);

                    // Display traffic estimates.
                    if (result != null && result.campaignEstimates != null &&
                        result.campaignEstimates.Length > 0)
                    {
                        CampaignEstimate campaignEstimate = result.campaignEstimates[0];

                        
                       
                    }
                    else
                    {
                        Console.WriteLine("No traffic estimates were returned.");
                    }
                    trafficEstimatorService.Close();
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to retrieve traffic estimates.", e);
                }
            }
        }

    }
}
