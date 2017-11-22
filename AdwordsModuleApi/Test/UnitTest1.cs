using System;
using System.Collections.Generic;
using System.Linq;
using AdwordsModuleApi.Adwords;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetCampaignsTest()
        {
            // Arrange
            List<Campaign> campaigns = Campaigns.GetCampaigns(new AdWordsUser());

            // Act
            Campaign[] camp = campaigns.Where(campa => campa.name == "Test Kampagne").ToArray();

            // Assert
            Assert.AreNotEqual(0, campaigns.Count);
            Assert.AreEqual(980217086, camp[0].id);
        }
    }
}
