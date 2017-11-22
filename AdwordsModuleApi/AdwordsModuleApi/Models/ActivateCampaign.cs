using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdwordsModuleApi.Adwords;

namespace AdwordsModuleApi.Models
{
    public class ActivateCampaign
    {
        public int CampaignId { get; set; }

        public List<ExpandedTextAdDto> ExpandedTextAd { get; set; }

        public ActivateCampaign()
        {
            ExpandedTextAd = new List<ExpandedTextAdDto>();
        }

    }
}