using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    public class ExpandedTextAdDto
    {
        public string HeadLinePart1 { get; set; }
        public string HeadLinePart2 { get; set; }
        public string Description { get; set; }
        public string[] FinalUrls { get; set; }
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }
    }
}