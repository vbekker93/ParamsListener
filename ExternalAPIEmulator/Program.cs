using Infrastructure.Models;
using Newtonsoft.Json;
using ParamsListenerWebPortal.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExternalAPIEmulator
{
    internal class Program
    {
        /// <summary>
        /// Генератор случайных величин
        /// </summary>
        private static readonly Random random = new Random();

        /// <summary>
        /// Буфер объектов БД
        /// </summary>
        private static List<ParamsEntity> DbEntities;

        private static void Main(string[] args)
        {
            InitParams();

            while (true)
            {
                UpdateAndSendRandomEntity(SettingsApp.Default.MessageCountOnPeriod);
                Thread.Sleep(SettingsApp.Default.UpdatePeriodWait);
            }
        }

        /// <summary>
        /// Инициализация объектов БД в сервисе
        /// </summary>
        private static void InitParams()
        {
            DbEntities = WebApiHelper.ExecuteWebApiRequest<List<ParamsEntity>>(SettingsApp.Default.DefaultHostGet,
                                                                                     "api/ParamsEntities",
                                                                                     WebApiHelper.HttpMethod.GET)?.Result;
            int missedEntitiesCount;

            if (DbEntities == null)
                missedEntitiesCount = SettingsApp.Default.MaxEntities;
            else
                missedEntitiesCount = SettingsApp.Default.MaxEntities - DbEntities.Count;

            if (missedEntitiesCount > 0)
            {
                for (int i = 0; i < missedEntitiesCount; i++)
                    UpdateRandomEntity(GetNewRandomEntity(), true);

                DbEntities = WebApiHelper.ExecuteWebApiRequest<List<ParamsEntity>>(SettingsApp.Default.DefaultHostGet,
                                                                                     "api/ParamsEntities",
                                                                                     WebApiHelper.HttpMethod.GET)?.Result;
            }
        }

        /// <summary>
        /// Обновить и отправить количество случайных сущностей сервису
        /// </summary>
        /// <param name="messageCount">Количество сущностей</param>
        private static void UpdateAndSendRandomEntity(int messageCount)
        {
            if (DbEntities.Count < messageCount)
                InitParams();

            List<int> updatedId = new List<int>();

            for (int i = 0; i < messageCount; i++)
            {
                int randomElemPos;
                bool isInList;

                do
                {
                    randomElemPos = random.Next(DbEntities.Count);
                    isInList = updatedId.IndexOf(randomElemPos) != -1;
                }
                while (isInList);

                long oldId = DbEntities[randomElemPos].Id;
                DbEntities[randomElemPos] = GetNewRandomEntity();
                DbEntities[randomElemPos].Id = oldId;
                updatedId.Add(randomElemPos);

                UpdateRandomEntity(DbEntities[randomElemPos], false);
            }
        }

        /// <summary>
        /// Обновить объект сущности
        /// </summary>
        /// <param name="entity">Объект сущности</param>
        /// <param name="isCreate">Создать объект в БД</param>
        private static void UpdateRandomEntity(ParamsEntity entity, bool isCreate)
        {
            try
            {
                WebApiHelper.HttpMethod _method;
                string uri;

                if (!isCreate)
                {
                    _method = WebApiHelper.HttpMethod.PUT;
                    uri = $"api/ParamsEntities/{entity.Id}";
                }
                else
                {
                    _method = WebApiHelper.HttpMethod.POST;
                    uri = $"api/ParamsEntities";
                }

                /// Конвертация в Json-тело запроса
                string jsonMeet = JsonConvert.SerializeObject(entity);
                StringContent bodyPostData = new StringContent(jsonMeet, Encoding.UTF8, WebApiHelper.BaseMediaType);
                Task<string> task = WebApiHelper.ExecuteWebApiRequest<string>("https://localhost:44352", uri, _method, bodyPostData);
                task.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Получить объект сущности со случайными значениями параметров
        /// </summary>
        /// <returns>Объект сущности</returns>
        private static ParamsEntity GetNewRandomEntity()
        {
            ParamsEntity entity = new ParamsEntity();
            entity.Param1 = GetRandomNumber(-1.0, 1.0);
            entity.Param2 = GetRandomNumber(-1.0, 1.0);
            entity.Param3 = GetRandomNumber(-1.0, 1.0);
            entity.Param4 = GetRandomNumber(-1.0, 1.0);
            entity.Param5 = GetRandomNumber(-1.0, 1.0);
            entity.Param6 = GetRandomNumber(-1.0, 1.0);
            entity.Param7 = GetRandomNumber(-1.0, 1.0);
            entity.Param8 = GetRandomNumber(-1.0, 1.0);
            entity.Param9 = GetRandomNumber(-1.0, 1.0);
            entity.Param10 = GetRandomNumber(-1.0, 1.0);
            entity.Param11 = GetRandomNumber(-1.0, 1.0);
            entity.Param12 = GetRandomNumber(-1.0, 1.0);
            entity.Param13 = GetRandomNumber(-1.0, 1.0);
            entity.Param14 = GetRandomNumber(-1.0, 1.0);
            entity.Param15 = GetRandomNumber(-1.0, 1.0);
            entity.Param16 = GetRandomNumber(-1.0, 1.0);
            entity.Param17 = GetRandomNumber(-1.0, 1.0);
            entity.Param18 = GetRandomNumber(-1.0, 1.0);
            entity.Param19 = GetRandomNumber(-1.0, 1.0);
            entity.Param20 = GetRandomNumber(-1.0, 1.0);

            return entity;
        }

        /// <summary>
        /// Получить случайное дробное число в диапазоне
        /// </summary>
        /// <param name="minimum">Нижняя граница диапазона</param>
        /// <param name="maximum">Верхняя граница диапазона</param>
        /// <returns></returns>
        private static double GetRandomNumber(double minimum, double maximum)
        {
            return Math.Round(random.NextDouble() * (maximum - minimum) + minimum, 2);
        }
    }
}