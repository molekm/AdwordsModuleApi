using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    public class KeyValuePair
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public ProductLo ProductLo { get; set; }
        public int ProductLoId { get; set; }

    }
}