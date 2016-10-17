using System.Web;
using System.Web.Mvc;

namespace FINAL_CASESTUDY
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
