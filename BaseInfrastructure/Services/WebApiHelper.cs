using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ParamsListenerWebPortal.Helpers
{
    public static class WebApiHelper
    {
        /// <summary>
        /// Перечисление для обозначения типа HTTP-запроса
        /// </summary>
        public enum HttpMethod
        {
            GET = 3,
            POST = 0,
            PUT = 1,
            DELETE = 2
        }

        /// <summary>
        /// Общий тип передаваемых данных
        /// </summary>
        public const string BaseMediaType = "application/json";

        /// <summary>
        /// Метод вызова Web-Api функций
        /// Получает данные для моделей
        /// </summary>
        /// <typeparam name="T">Тип объекта данных</typeparam>
        /// <param name="apiUri">Ссылка на API функцию</param>
        /// <param name="httpMethod">Тип запроса</param>
        /// <param name="content">Данные для тела запроса (post)</param>
        /// <returns>Объект данных типа Т</returns>
        public static async Task<T> ExecuteWebApiRequest<T>(string host, string apiUri, HttpMethod httpMethod, StringContent content = null)
        {
            T responceObject = default(T);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responceMessage;
                    client.BaseAddress = new Uri(host);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(BaseMediaType));

                    if (httpMethod == HttpMethod.POST && content != null)
                    {
                        responceMessage = await client.PostAsync(apiUri, content);
                    }
                    else if (httpMethod == HttpMethod.DELETE)
                    {
                        responceMessage = await client.DeleteAsync(apiUri);
                    }
                    else
                        responceMessage = await client.GetAsync(apiUri);

                    if (responceMessage.IsSuccessStatusCode)
                    {
                        string responseResult = responceMessage.Content.ReadAsStringAsync().Result;
                        responceObject = JsonConvert.DeserializeObject<T>(responseResult);
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO: Добавить лог
            }

            return responceObject;
        }
    }
}