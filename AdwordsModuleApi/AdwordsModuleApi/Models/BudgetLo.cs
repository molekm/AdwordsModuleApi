using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    interface IBudgetLo
    {
        string Name { get; set; }
        long MicroAmount { get; set; }
    }
    public class BudgetLo : IBudgetLo
    {
        public string Name { get; set; }
        public long MicroAmount { get; set; }
    }
}