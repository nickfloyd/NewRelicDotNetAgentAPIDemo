using System.Web.Http;

namespace NewRelicDotNetAgentAPIDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "NewRelicAPI",
                "api/{controller}/{action}"
            );
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

        }
    }
}
