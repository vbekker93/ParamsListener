using Infrastructure.Models;
using ParamsListenerWebPortal.Helpers;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace ParamsListenerWebPortal.Controllers
{
    /// <summary>
    /// Контроллер отображения таблицы с данными сущностей
    /// </summary>
    public class ParamsEntityController : Controller
    {
        /// <summary>
        /// Инициализация страницы с данными о сущностях
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            /// Авто-обновление данных страницы
            Response.AddHeader("Refresh", "10");
        }

        /// <summary>
        /// Получение контента данных представления
        /// </summary>
        /// <returns>Контент таблицы сущностей</returns>
        public async Task<ActionResult> Index()
        {
            return View(await WebApiHelper.ExecuteWebApiRequest<List<ParamsEntity>>(ConfigurationManager.AppSettings["ServiceHost"],
                                                                                    "api/ParamsEntities",
                                                                                    WebApiHelper.HttpMethod.GET));
        }
    }
}