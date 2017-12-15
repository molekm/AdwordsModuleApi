using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;
using Microsoft.Ajax.Utilities;

namespace AdwordsModuleApi.Adwords
{
    public static class Campaigns
    {
        public static CampaignReturnValue CreateCampaign(AdWordsUser user, CampaignLo campaignDto)
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
                    campaign.status = CampaignStatus.ENABLED;

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
                    if(Debugger.IsAttached)
                    campaignDto.StartDate = campaignDto.StartDate.AddHours(6);
                    campaign.startDate = campaignDto.StartDate.ToString("yyyyMMdd");

                    // Optional: Set the end date.
                    if(Debugger.IsAttached)
                    campaignDto.EndDate = campaignDto.EndDate.AddHours(6);
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

        public static List<Campaign> GetCampaigns(AdWordsUser user, bool isUnitTest)
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
                        Campaign.Fields.Status,
                        Campaign.Fields.StartDate,
                        Campaign.Fields.EndDate,
                        Budget.Fields.Amount
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
                                if (campaign.status == CampaignStatus.ENABLED && CampaignEnded(campaign.endDate, isUnitTest))
                                {
                                    campaign.startDate = !Debugger.IsAttached ? FormatDateStringUs(campaign.startDate) : FormatDateString(campaign.startDate);
                                    campaign.endDate = !Debugger.IsAttached ? FormatDateStringUs(campaign.endDate) : FormatDateString(campaign.endDate);
                                campaigns.Add(campaign);
                                }                                   
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

        public static bool CampaignEnded(string endDate, bool isUnitTest)
        {
            DateTime date, dateNow;

            try
            {
                if(!isUnitTest)
                    date = Convert.ToDateTime(!Debugger.IsAttached ? FormatDateStringUs(endDate) : FormatDateString(endDate));
                else
                    date = Convert.ToDateTime(Debugger.IsAttached ? FormatDateStringUs(endDate) : FormatDateString(endDate));

                dateNow = DateTime.Now;
            }
            catch (Exception e)
            {
                throw new System.ApplicationException("Failed to retrieve dates", e);
            }
            return date > dateNow;
        }

        public static string FormatDateString(string datestring)
        {
            string year, month, day;

            try
            {
                year = datestring.Substring(0, 4);
                month = datestring.Substring(4, 2);
                day = datestring.Substring(6, 2);
            }
            catch (Exception e)
            {
                throw new System.ApplicationException("Failed to format date string", e);
            }
            return $"{day}/{month}/{year}";
        }

        public static string FormatDateStringUs(string datestring)
        {
            string year, month, day;

            try
            {
                year = datestring.Substring(0, 4);
                month = datestring.Substring(4, 2);
                day = datestring.Substring(6, 2);
            }
            catch (Exception e)
            {
                throw new System.ApplicationException("Failed to format date string", e);
            }
            return $"{month}/{day}/{year}";
        }

        public static CampaignReturnValue SetCampaignStatus(AdWordsUser user, long campaignId, CampaignStatus campaignStatus)
        {
            CampaignReturnValue retVal;
            using (CampaignService campaignService =
                (CampaignService)user.GetService(AdWordsService.v201710.CampaignService))
            {

                // Create the campaign.
                Campaign campaign = new Campaign();
                campaign.id = campaignId;
                campaign.status = campaignStatus;

                // Create the operation.
                CampaignOperation operation = new CampaignOperation();
                operation.@operator = Operator.SET;
                operation.operand = campaign;

                try
                {
                    // Update the campaign.
                    retVal = campaignService.mutate(
                        new CampaignOperation[] { operation });

                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to update campaign.", e);
                }
                return retVal;
            }
        }
    }

}