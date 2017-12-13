using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    public class AdGroupLo
    {
        public long AdGroupId { get; set; }
        public string Name { get; set; }
        public string KeyWords { get; set; }
        public long CampaignId { get; set; }


    }
}