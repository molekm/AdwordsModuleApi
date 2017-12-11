using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdwordsModuleApi.Models
{
    public class AdWordsContentLo
    {
        public AdGroupLo AdGroupLo { get; set; }
        public List<ProductItemLo> ContentProducts { get; set; }

    }
}