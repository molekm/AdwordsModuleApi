﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;

namespace AdwordsModuleApi.Adwords
{
    public static class Campaigns
    {

        public static CampaignReturnValue CreateCampaign(AdWordsUser user, CampaignDto campaignDto)
        {
            using (CampaignService campaignService =
                (CampaignService)user.GetService(AdWordsService.v201710.CampaignService))
            {
                CampaignReturnValue camp = new CampaignReturnValue();

                Budget budget = Budgets.CreateBudget(user, campaignDto);

                List<CampaignOperation> operations = new List<CampaignOperation>();

                
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

                    campaign.settings = new Setting[] { geoSetting };

                    // Optional: Set the start date.
                    campaignDto.StartDate = campaignDto.StartDate.AddHours(12);
                    campaign.startDate = campaignDto.StartDate.ToString("yyyyMMdd");

                    // Optional: Set the end date.
                    campaignDto.EndDate = campaignDto.EndDate.AddHours(12);
                    campaign.endDate = campaignDto.EndDate.ToString("yyyyMMdd");

                    // Optional: Set the frequency cap.
                    FrequencyCap frequencyCap = new FrequencyCap();
                    frequencyCap.impressions = 5;
                    frequencyCap.level = Level.ADGROUP;
                    frequencyCap.timeUnit = TimeUnit.DAY;
                    campaign.frequencyCap = frequencyCap;

                    // Create the operation.
                    CampaignOperation operation = new CampaignOperation();
                    operation.@operator = Operator.ADD;
                    operation.operand = campaign;

                    operations.Add(operation);
                

                try
                {
                    // Add the campaign.
                    camp = campaignService.mutate(operations.ToArray());                   
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to add campaigns.", e);
                }
                return camp;
            }
        }

        public static List<Campaign> GetCampaigns(AdWordsUser user)
        {
            using (CampaignService campaignService =
                (CampaignService)user.GetService(AdWordsService.v201710.CampaignService))
            {
                List<Campaign> campaigns = new List<Campaign>();
                // Create the selector.
                Selector selector = new Selector()
                {
                    fields = new string[] {
                        Campaign.Fields.Id,
                        Campaign.Fields.Name,
                        Campaign.Fields.Status
                    },
                    paging = Paging.Default
                };

                CampaignPage page = new CampaignPage();

                try
                {
                        // Get the campaigns.
                        page = campaignService.get(selector);
                   
                        // Display the results.
                        if (page?.entries != null)
                        {
                            foreach (Campaign campaign in page.entries)
                            {
                                if(campaign.status != CampaignStatus.ENABLED)
                                    campaigns.Add(campaign);
                            }
                        }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to retrieve campaigns", e);
                }
                return campaigns;
            }
        }
    }
}