using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    public class CampaignLo
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

    public class CampaignItem
    {
        public int Id { get; set; }
        public long MicroAmount { get; set; }
        public string name { get; set; }
    }
}