using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    interface IKeyValuePairLo
    {
        int Id { get; set; }
        string Key { get; set; }
        string Value { get; set; }
        ProductLo ProductLo { get; set; }
        int ProductLoId { get; set; }
    }
    public class KeyValuePairLo : IKeyValuePairLo
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public ProductLo ProductLo { get; set; }
        public int ProductLoId { get; set; }

    }
}