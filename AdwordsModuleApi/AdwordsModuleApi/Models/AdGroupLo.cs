using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    interface IAdGroupLo
    {
        long AdGroupId { get; set; }
        string Name { get; set; }
        string KeyWords { get; set; }
        long CampaignId { get; set; }
    }
    public class AdGroupLo : IAdGroupLo
    {
        public long AdGroupId { get; set; }
        public string Name { get; set; }
        public string KeyWords { get; set; }
        public long CampaignId { get; set; }
    }
}