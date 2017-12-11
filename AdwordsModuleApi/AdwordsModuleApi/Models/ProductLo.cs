using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    public class ProductLo
    {
        public int Id { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public string LogicName { get; set; }
        public string Description { get; set; }
        public string DescriptionShort { get; set; }
        public ProductGroupLo AdGroupLo { get; set; }
        public int AdGroupLoId { get; set; }
        public List<KeyValuePairLo> KeyValuePairs { get; set; }
    }
}