using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    interface IProductGroupLo
    {
        int Id { get; set; }
        string GroupName { get; set; }
        List<ProductLo> ProductLos { get; set; }
    }
    public class ProductGroupLo : IProductGroupLo
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