using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;

namespace AdwordsModuleApi.Adwords
{
    public class AdGroupAdwords
    {
        static int count = 0;
        public static AdGroupReturnValue CreateAdGroup(AdWordsUser user, AdGroupLo adGroupLo )
        {
            using (AdGroupService adGroupService =
                (AdGroupService)user.GetService(AdWordsService.v201710.AdGroupService))
            {
                List<AdGroupOperation> operations = new List<AdGroupOperation>();
                
                    // Create the ad group.
                    AdGroup adGroup = new AdGroup();
                    adGroup.name = adGroupLo.Name;
                    adGroup.status = AdGroupStatus.PAUSED;
                    adGroup.campaignId = adGroupLo.CampaignId;

                    // Set the ad group bids.
                    BiddingStrategyConfiguration biddingConfig = new BiddingStrategyConfiguration();

                    CpcBid cpcBid = new CpcBid();
                    cpcBid.bid = new Money();
                    cpcBid.bid.microAmount = 10000000;

                    biddingConfig.bids = new Bids[] { cpcBid };

                    adGroup.biddingStrategyConfiguration = biddingConfig;

                    // Optional: Set targeting restrictions.
                    // Depending on the criterionTypeGroup value, most TargetingSettingDetail
                    // only affect Display campaigns. However, the USER_INTEREST_AND_LIST value
                    // works for RLSA campaigns - Search campaigns targeting using a
                    // remarketing list.
                    TargetingSetting targetingSetting = new TargetingSetting();

                    // Restricting to serve ads that match your ad group placements.
                    // This is equivalent to choosing "Target and bid" in the UI.
                    TargetingSettingDetail placementDetail = new TargetingSettingDetail();
                    placementDetail.criterionTypeGroup = CriterionTypeGroup.PLACEMENT;
                    placementDetail.targetAll = false;

                    // Using your ad group verticals only for bidding. This is equivalent
                    // to choosing "Bid only" in the UI.
                    TargetingSettingDetail verticalDetail = new TargetingSettingDetail();
                    verticalDetail.criterionTypeGroup = CriterionTypeGroup.VERTICAL;
                    verticalDetail.targetAll = true;

                    targetingSetting.details = new TargetingSettingDetail[] {
                        placementDetail, verticalDetail
                    };

                    adGroup.settings = new Setting[] { targetingSetting };

                    // Set the rotation mode.
                    AdGroupAdRotationMode rotationMode = new AdGroupAdRotationMode();
                    rotationMode.adRotationMode = AdRotationMode.OPTIMIZE;
                    adGroup.adGroupAdRotationMode = rotationMode;

                    // Create the operation.
                    AdGroupOperation operation = new AdGroupOperation();
                    operation.@operator = Operator.ADD;
                    operation.operand = adGroup;

                    operations.Add(operation);


                AdGroupReturnValue returnDGroup;
                try
                {
                    // Create the ad group.
                    returnDGroup = adGroupService.mutate(operations.ToArray());

                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to create ad group.", e);
                }
                return returnDGroup;
            }
        }

        public static List<AdGroup> GetAdGroups(AdWordsUser user, long campaignId)
        {
            using (AdGroupService adGroupService =
                (AdGroupService)user.GetService(AdWordsService.v201710.AdGroupService))
            {

                // Create the selector.
                Selector selector = new Selector()
                {
                    fields = new string[] { AdGroup.Fields.Id, AdGroup.Fields.Name, AdGroup.Fields.CampaignId, AdGroup.Fields.Status },
                    predicates = new Predicate[] {
                        Predicate.Equals(AdGroup.Fields.CampaignId, campaignId)
                    },
                    paging = Paging.Default,
                    ordering = new OrderBy[] { OrderBy.Asc(AdGroup.Fields.Name) }
                };

                AdGroupPage page = new AdGroupPage();

                List<AdGroup> result = new List<AdGroup>();

                try
                {
                        // Get the ad groups.
                        page = adGroupService.get(selector);

                    if(page.entries != null)
                    {
                        foreach (AdGroup item in page.entries)
                        {
                            if (item.status == AdGroupStatus.PAUSED)
                            {
                                result.Add(item);
                            }
                        }
                    }
                    
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to retrieve ad groups.", e);
                }
                return result;
            }
        }
        public static AdGroupReturnValue SetAdGroupStatus(AdWordsUser user, long adGroupId, AdGroupStatus adGroupStatus)
        {
            using (AdGroupService adGroupService = (AdGroupService)user.GetService(
                AdWordsService.v201710.AdGroupService))
            {

                // Create ad group with REMOVED status.
                AdGroup adGroup = new AdGroup();
                adGroup.id = adGroupId;
                adGroup.status = adGroupStatus;

                // Create the operation.
                AdGroupOperation operation = new AdGroupOperation();
                operation.operand = adGroup;
                operation.@operator = Operator.SET;

                AdGroupReturnValue retVal;

                try
                {
                    // Remove the ad group.
                    retVal = adGroupService.mutate(new AdGroupOperation[] { operation });

                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to remove ad group.", e);
                }
                return retVal;
            }
        }
    }
}