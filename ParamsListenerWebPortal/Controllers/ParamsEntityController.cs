using Infrastructure.Models;
using ParamsListenerWebPortal.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ParamsListenerWebPortal.Controllers
{
    public class ParamsEntityController : Controller
    {
        // GET: ParamsEntity
        public async Task<ActionResult> Index()
        {
            return View(await WebApiHelper.ExecuteWebApiRequest<List<ParamsEntity>>("api/ParamsEntities", WebApiHelper.HttpMethod.GET));
        }

    }
}
