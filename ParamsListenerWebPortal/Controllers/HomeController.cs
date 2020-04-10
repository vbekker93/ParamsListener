using System.Web.Mvc;

namespace ParamsListenerWebPortal.Controllers
{
    /// <summary>
    /// Контроллер страницы информации о портале
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Вернуть заголовок представления About
        /// </summary>
        /// <returns>Контент</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Test task WebPortal for Params Listener.";

            return View();
        }
    }
}