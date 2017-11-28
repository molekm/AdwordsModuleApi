using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    public class CampaignDto
    {
        public CampaignDto()
        {
            Budget = new BudgetDto();
        }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BudgetDto Budget { get; set; }
    }

    public class CampaignItem
    {
        public int Id { get; set; }
        public long MicroAmount { get; set; }
        public string name { get; set; }
    }
}