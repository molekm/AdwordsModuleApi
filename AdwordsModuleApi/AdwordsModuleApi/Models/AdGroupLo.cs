using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    public class AdGroupLo
    {
        public long adGroupId { get; set; }
        public string Name { get; set; }
        public string KeyWords { get; set; }
        public long CampaignId { get; set; }


    }
}