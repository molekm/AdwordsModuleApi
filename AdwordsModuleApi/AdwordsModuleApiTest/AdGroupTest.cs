using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdwordsModuleApi.Adwords;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdwordsModuleApiTest
{
    [TestClass]
    public class AdGroupTest
    {
        [TestMethod]
        public void CreateAdGroupTest()
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

            // Act
            var result = AdGroupAdwords.CreateAdGroup(new AdWordsUser(), adGroupDto);

            // Assert
            Assert.AreEqual(adGroupDto.CampaignId, result.value[0].campaignId);
            Assert.AreEqual(2, (int)result.value[0].status);
        }

        [TestMethod]
        public void AddKeyWordsToAdGroup()
        {
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

            AdGroupLo adGroupDotLo = new AdGroupLo
            {
                CampaignId = campaign.value[0].id,
                Name = DateTime.Now.ToString(),
                KeyWords = "hej, med dig"
            };
            var addedAdGroup = AdGroupAdwords.CreateAdGroup(new AdWordsUser(), adGroupDotLo);

            // Act
            var result = AdWordsKeyword.AddKeyWordsToAdGroup(new AdWordsUser(), addedAdGroup.value[0].id, adGroupDotLo.KeyWords);

            // Assert
            Assert.AreEqual(2, result.value.Length);
        }

        [TestMethod]
        public void GetAdGroups()
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

            AdGroupLo adGroupDtoLo = new AdGroupLo
            {
                CampaignId = campaign.value[0].id,
                Name = DateTime.Now.ToString(),
                KeyWords = "hej, med dig"
            };

            // Act
            var adGroup = AdGroupAdwords.CreateAdGroup(new AdWordsUser(), adGroupDtoLo);
            var result = AdGroupAdwords.GetAdGroups(new AdWordsUser(), campaign.value[0].id);

            // Assert
            Assert.AreNotEqual(0, result.Count);
            Assert.AreEqual(adGroup.value[0].name, result.ElementAt(0).name);
        }

        [TestMethod]
        public void SetAdGroupStatus()
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

            AdGroupLo adGroupDtoLo = new AdGroupLo
            {
                CampaignId = campaign.value[0].id,
                Name = DateTime.Now.ToString(),
                KeyWords = "hej, med dig"
            };

            // Act
            var adGroup = AdGroupAdwords.CreateAdGroup(new AdWordsUser(), adGroupDtoLo);
            var result = AdGroupAdwords.SetAdGroupStatus(new AdWordsUser(), adGroup.value[0].id, AdGroupStatus.REMOVED);

            // Assert
            Assert.AreEqual(3, (int)result.value[0].status);
        }
    }
}
