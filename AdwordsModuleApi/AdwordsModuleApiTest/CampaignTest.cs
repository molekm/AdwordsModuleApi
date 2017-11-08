using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdwordsModuleApi.Adwords.Test;
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
            CampaignDto campaignDto = new CampaignDto
            {
                Name = "Test Campaign",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1),
                BudgetDto = new BudgetDto
                {
                    Name = "Test Budget",
                    MicroAmount = 50
                }
            };

            // Act
            Campaign campaign =  TestCampaigns.CreateCampaignTest(new AdWordsUser(), campaignDto);

            // Assert
            Assert.AreEqual(DateTime.Now.ToString("yyyyMMdd"), campaign.startDate);
            Assert.AreEqual(DateTime.Now.AddYears(1).ToString("yyyyMMdd"), campaign.endDate);
            Assert.AreEqual(2, (int)campaign.status);
            Assert.AreEqual(true, campaign.networkSetting.targetGoogleSearch);
            Assert.AreEqual(false, campaign.networkSetting.targetPartnerSearchNetwork);

        }

        [TestMethod]
        public void CreateBudgetTest()
        {
            // Arrange
            CampaignDto campaignDto = new CampaignDto
            {
                Name = "Test Campaign",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1),
                BudgetDto = new BudgetDto
                {
                    Name = "Test Budget",
                    MicroAmount = 50
                }
            };

            // Act
            Budget budget = TestCampaigns.CreateBudgetTest(new AdWordsUser(), campaignDto);

            Assert.AreEqual(50, budget.amount.microAmount);
            Assert.AreEqual(false, budget.isExplicitlyShared);
            Assert.AreEqual(0, (int)budget.deliveryMethod);
        }
    }
}
