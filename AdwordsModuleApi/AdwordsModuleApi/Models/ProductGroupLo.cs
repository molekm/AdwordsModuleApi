using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    public class ProductGroupLo
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<ProductLo> ProductLos { get; set; }

        public ProductGroupLo()
        {
            ProductLos = new List<ProductLo>();
        }
    }
}