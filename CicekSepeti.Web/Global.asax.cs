using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace CicekSepeti.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            var container = new UnityContainer();
            CicekSepeti.IOC.App_Start.UnityConfig.RegisterTypes(container);
        }

    }
}
