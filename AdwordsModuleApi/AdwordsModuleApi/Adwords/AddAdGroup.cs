using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;

namespace AdwordsModuleApi.Adwords
{
    public class AddAdGroup
    {
        static int count = 0;
        public static AdGroupReturnValue CreateAdGroup(AdWordsUser user, int campaignId, ProductItem expandedTextAd, long amount)
        {
            using (AdGroupService adGroupService =
                (AdGroupService)user.GetService(AdWordsService.v201710.AdGroupService))
            {
                List<AdGroupOperation> operations = new List<AdGroupOperation>();
                
                    // Create the ad group.
                    AdGroup adGroup = new AdGroup();
                    adGroup.name = "adGroupName";
                    adGroup.status = AdGroupStatus.ENABLED;
                    adGroup.campaignId = campaignId;

                    // Set the ad group bids.
                    BiddingStrategyConfiguration biddingConfig = new BiddingStrategyConfiguration();

                    CpcBid cpcBid = new CpcBid();
                    cpcBid.bid = new Money();
                    cpcBid.bid.microAmount = amount;

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
    }
}