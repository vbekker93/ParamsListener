using Infrastructure.Models;
using ParamsListenerWebPortal.Helpers;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParamsListenerWebPortal.Controllers
{
    public class ParamsEntityController : Controller
    {
        // GET: ParamsEntity
        public async Task<ActionResult> Index()
        {
            Response.AddHeader("Refresh", "5");

            return View(await WebApiHelper.ExecuteWebApiRequest<List<ParamsEntity>>(ConfigurationManager.AppSettings["ServiceHost"],
                                                                                    "api/ParamsEntities",
                                                                                    WebApiHelper.HttpMethod.GET));
        }
    }
}