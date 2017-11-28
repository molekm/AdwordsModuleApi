using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    public class AdWordsContent
    {
        public CampaignItem ContentCampaign { get; set; }
        public List<ProductItem> ContentProducts { get; set; }

    }
}