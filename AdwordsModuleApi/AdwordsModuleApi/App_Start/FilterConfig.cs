using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace AdwordsModuleApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            
        }
        
    }
}
