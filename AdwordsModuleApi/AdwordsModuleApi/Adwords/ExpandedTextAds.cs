using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;

namespace AdwordsModuleApi.Adwords
{
    public class ExpandedTextAds
    {
        public static AdGroupAdReturnValue CreateTextAdd(AdWordsUser user, long adGroupId, ExpandedTextAdDto expandedTextAdDto)
        {
            using (AdGroupAdService adGroupAdService =
                (AdGroupAdService)user.GetService(AdWordsService.v201710.AdGroupAdService))
            {

                List<AdGroupAdOperation> operations = new List<AdGroupAdOperation>();

                    // Create the expanded text ad.
                    ExpandedTextAd expandedTextAd = new ExpandedTextAd();
                    expandedTextAd.headlinePart1 = expandedTextAdDto.HeadLinePart1;
                    expandedTextAd.headlinePart2 = expandedTextAdDto.HeadLinePart2;
                    expandedTextAd.description = expandedTextAdDto.Description;
                    expandedTextAd.finalUrls = expandedTextAdDto.FinalUrls;

                    AdGroupAd expandedTextAdGroupAd = new AdGroupAd();
                    expandedTextAdGroupAd.adGroupId = adGroupId;
                    expandedTextAdGroupAd.ad = expandedTextAd;

                    // Optional: Set the status.
                    expandedTextAdGroupAd.status = AdGroupAdStatus.ENABLED;

                    // Create the operation.
                    AdGroupAdOperation operation = new AdGroupAdOperation();
                    operation.@operator = Operator.ADD;
                    operation.operand = expandedTextAdGroupAd;

                    operations.Add(operation);

                AdGroupAdReturnValue retVal = null;

                try
                {
                    // Create the ads.
                    retVal = adGroupAdService.mutate(operations.ToArray());

                   
                    adGroupAdService.Close();
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to create expanded text ad.", e);
                }
                return retVal;
            }
        }
    }
}