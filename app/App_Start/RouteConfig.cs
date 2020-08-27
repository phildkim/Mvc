using System.Web.Mvc;
using System.Web.Routing;
namespace app
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Trader",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Trader", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}