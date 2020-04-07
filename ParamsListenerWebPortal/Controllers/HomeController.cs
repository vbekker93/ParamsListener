using System.Web.Mvc;

namespace ParamsListenerWebPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "Test task WebPortal for Params Listener.";

            return View();
        }
    }
}