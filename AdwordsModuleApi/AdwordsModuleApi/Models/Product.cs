using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public string LogicName { get; set; }
        public string Description { get; set; }
        public string ExtraDescription { get; set; }
    }

    public class ProductItem
    {
        public AdContent AdContent { get; set; }
        public Product Product { get; set; }
        public string[] FinalUrl { get; set; }

    }
}