using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdwordsModuleApi.Adwords;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdwordsModuleApiTest
{
    [TestClass]
    public class AdsTest
    {
        [TestMethod]
        public void AddAds()
        {
            // Assert
            CampaignLo campaignDto = new CampaignLo
            {
                Name = DateTime.Now.ToString(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1),
                Budget = new BudgetLo
                {
                    Name = DateTime.Now.ToString(),
                    MicroAmount = 5000000
                }
            };
            var campaign = Campaigns.CreateCampaign(new AdWordsUser(), campaignDto);

            AdGroupLo adGroupDto = new AdGroupLo
            {
                CampaignId = campaign.value[0].id,
                Name = DateTime.Now.ToString(),
                KeyWords = "hej, med dig"
            };
            var adGroup = AdGroupAdwords.CreateAdGroup(new AdWordsUser(), adGroupDto);

            AdWordsContentLo adsContent = new AdWordsContentLo
            {
                AdGroupLo = new AdGroupLo
                {
                    AdGroupId = adGroup.value[0].id,
                    Name = adGroup.value[0].name
                },
                ContentProducts = new List<ProductItemLo>()
                {
                    
                }
            };

            // Act

        }
    }
}
