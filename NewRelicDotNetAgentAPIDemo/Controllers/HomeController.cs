using System.Threading;
using System.Web.Mvc;

namespace NewRelicDotNetAgentAPIDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Using the New Relic Agent API";
            NewRelic.Api.Agent.NewRelic.SetUserParameters("Nick", "SomeUserHandle", "NewRelicEcomSite");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            Thread.Sleep(5000);

            NewRelic.Api.Agent.NewRelic.SetUserParameters("Nick", "SomeUserHandle", "NewRelicEcomSite");
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            NewRelic.Api.Agent.NewRelic.SetUserParameters("Nick", "SomeUserHandle", "NewRelicEcomSite");
            return View();
        }
    }
}
