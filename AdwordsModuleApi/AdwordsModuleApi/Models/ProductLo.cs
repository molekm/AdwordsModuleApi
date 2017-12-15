using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    interface IProductLo
    {
        int Id { get; set; }
        string ProductNumber { get; set; }
        string ProductName { get; set; }
        string LogicName { get; set; }
        string Description { get; set; }
        string DescriptionShort { get; set; }
        ProductGroupLo AdGroupLo { get; set; }
        int AdGroupLoId { get; set; }
        List<KeyValuePairLo> KeyValuePairs { get; set; }
    }
    public class ProductLo : IProductLo
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