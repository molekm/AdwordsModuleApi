using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    interface ICampaignLo
    {
        string Name { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        BudgetLo Budget { get; set; }
    }
    public class CampaignLo : ICampaignLo
    {
        public CampaignLo()
        {
            Budget = new BudgetLo();
        }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BudgetLo Budget { get; set; }
    }
}