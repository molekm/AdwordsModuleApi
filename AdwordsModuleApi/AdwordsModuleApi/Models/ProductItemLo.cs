using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    public class ProductItemLo
    {
        public AdContentLo AdContent { get; set; }
        public int Id { get; set; }
        public string[] FinalUrl { get; set; }
    }
}