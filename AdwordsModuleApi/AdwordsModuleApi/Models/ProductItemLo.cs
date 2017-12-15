using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    interface IProductItemLo
    {
        AdContentLo AdContent { get; set; }
        int Id { get; set; }
        string[] FinalUrl { get; set; }
    }
    public class ProductItemLo : IProductItemLo
    {
        public AdContentLo AdContent { get; set; }
        public int Id { get; set; }
        public string[] FinalUrl { get; set; }
    }
}