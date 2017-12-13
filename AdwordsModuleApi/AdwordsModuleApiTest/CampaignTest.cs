using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdwordsModuleApi.Adwords;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;

namespace AdwordsModuleApiTest
{
    [TestClass]
    public class CampaignTest
    {
        [TestMethod]
        public void CreateCampaignTest()
        {
            // Arrange
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

            // Act
            CampaignReturnValue campaign =  Campaigns.CreateCampaign(new AdWordsUser(), campaignDto);

            // Assert
            Assert.AreEqual(DateTime.Now.ToString("yyyyMMdd"), campaign.value[0].startDate);
            Assert.AreEqual(DateTime.Now.AddYears(1).ToString("yyyyMMdd"), campaign.value[0].endDate);
            Assert.AreEqual(1, (int)campaign.value[0].status);
            Assert.AreEqual(true, campaign.value[0].networkSetting.targetGoogleSearch);
            Assert.AreEqual(false, campaign.value[0].networkSetting.targetPartnerSearchNetwork);

        }

        [TestMethod]
        public void CreateBudgetTest()
        {
            // Arrange
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

            // Act
            Budget budget = Budgets.CreateBudget(new AdWordsUser(), campaignDto);

            // Assert
            Assert.AreEqual(5000000, budget.amount.microAmount);
            Assert.AreEqual(false, budget.isExplicitlyShared);
            Assert.AreEqual(0, (int)budget.deliveryMethod);
        }

        [TestMethod]
        public void GetCampaignsTest()
        {
            // Arrange
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


            // Act
            var campaign = Campaigns.CreateCampaign(new AdWordsUser(), campaignDto);
            List<Campaign> campaigns = Campaigns.GetCampaigns(new AdWordsUser());
            Campaign[] camp = campaigns.Where(campa => campa.name == campaignDto.Name).ToArray();

            // Assert
            Assert.AreNotEqual(0, campaigns.Count);
            Assert.AreEqual(campaign.value[0].id, camp[0].id);
        }

        [TestMethod]
        public void SetCampaignStatusTest()
        {
            // Arrange
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

            // Act
            var campaign = Campaigns.CreateCampaign(new AdWordsUser(), campaignDto);
            var result = Campaigns.SetCampaignStatus(new AdWordsUser(), campaign.value[0].id, CampaignStatus.REMOVED);

            // Assert
            Assert.AreEqual(3, (int)result.value[0].status);
        }
    }
}
