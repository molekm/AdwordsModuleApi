using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    interface IAdWordsContentLo
    {
        AdGroupLo AdGroupLo { get; set; }
        List<ProductItemLo> ContentProducts { get; set; }
    }
    public class AdWordsContentLo : IAdWordsContentLo
    {
        public AdGroupLo AdGroupLo { get; set; }
        public List<ProductItemLo> ContentProducts { get; set; }

    }
}