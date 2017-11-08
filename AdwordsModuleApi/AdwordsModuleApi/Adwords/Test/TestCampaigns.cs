using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;

namespace AdwordsModuleApi.Adwords.Test
{
    public class TestCampaigns
    {
        public static Campaign CreateCampaignTest(AdWordsUser user, CampaignDto campaignDto)
        {
            using (CampaignService campaignService =
                (CampaignService) user.GetService(AdWordsService.v201710.CampaignService))
            {
                Budget budget = CreateBudgetTest(user, campaignDto);

                // Create the campaign.
                Campaign campaign = new Campaign();
                campaign.name = campaignDto.Name;
                campaign.advertisingChannelType = AdvertisingChannelType.SEARCH;

                // Recommendation: Set the campaign to PAUSED when creating it to prevent
                // the ads from immediately serving. Set to ENABLED once you've added
                // targeting and the ads are ready to serve.
                campaign.status = CampaignStatus.PAUSED;

                BiddingStrategyConfiguration biddingConfig = new BiddingStrategyConfiguration();
                biddingConfig.biddingStrategyType = BiddingStrategyType.MANUAL_CPC;
                campaign.biddingStrategyConfiguration = biddingConfig;

                campaign.budget = new Budget();
                campaign.budget.budgetId = budget.budgetId;

                // Set the campaign network options.
                campaign.networkSetting = new NetworkSetting();
                campaign.networkSetting.targetGoogleSearch = true;
                campaign.networkSetting.targetSearchNetwork = true;
                campaign.networkSetting.targetContentNetwork = false;
                campaign.networkSetting.targetPartnerSearchNetwork = false;

                // Set the campaign settings for Advanced location options.
                GeoTargetTypeSetting geoSetting = new GeoTargetTypeSetting();
                geoSetting.positiveGeoTargetType = GeoTargetTypeSettingPositiveGeoTargetType.DONT_CARE;
                geoSetting.negativeGeoTargetType = GeoTargetTypeSettingNegativeGeoTargetType.DONT_CARE;

                campaign.settings = new Setting[] {geoSetting};

                // Optional: Set the start date.
                campaign.startDate = campaignDto.StartDate.ToString("yyyyMMdd");

                // Optional: Set the end date.
                campaign.endDate = campaignDto.EndDate.ToString("yyyyMMdd");

                // Optional: Set the frequency cap.
                FrequencyCap frequencyCap = new FrequencyCap();
                frequencyCap.impressions = 5;
                frequencyCap.level = Level.ADGROUP;
                frequencyCap.timeUnit = TimeUnit.DAY;
                campaign.frequencyCap = frequencyCap;

                return campaign;
            }
            
        }

        public static Budget CreateBudgetTest(AdWordsUser user, CampaignDto campaignDto)
        {
            using (BudgetService budgetService =
                (BudgetService)user.GetService(AdWordsService.v201710.BudgetService))
            {

                // Create the campaign budget.
                Budget budget = new Budget();
                budget.isExplicitlyShared = false;
                budget.name = campaignDto.BudgetDto.Name;
                budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
                budget.amount = new Money();
                budget.amount.microAmount = campaignDto.BudgetDto.MicroAmount;

                return budget;
            }
        }
    }
}