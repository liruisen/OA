using System.Web;
using System.Web.Mvc;
using LRS.OA.Web.Models;

namespace LRS.OA.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new MyExceptionAttribute());
        }
    }
}