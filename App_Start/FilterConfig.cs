using System.Web;
using System.Web.Mvc;
using testwebmvc.Filters;

namespace testwebmvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
