﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using AdwordsModuleApi.Models;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201710;

namespace AdwordsModuleApi.Controllers.Api.Campaigns
{
    [Route("api/campaign")]
    public class CampaignsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult CreateCampaign([FromBody]CampaignDto campaignDto)
        {
            CampaignReturnValue camp = Adwords.Campaigns.CreateCampaign(new AdWordsUser(), campaignDto);

            return Ok(camp);
        }

        [HttpGet]
        public IHttpActionResult GetCampaigns()
        {
            List<Campaign> campaigns = Adwords.Campaigns.GetCampaigns(new AdWordsUser());

            return Ok(campaigns);
        }
    }
}