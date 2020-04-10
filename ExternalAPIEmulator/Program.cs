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
        /// Буфер объектов БД
        /// </summary>
        private static List<ParamsEntity> DbEntities;

        private static void Main(string[] args)
        {
            Console.WriteLine("Emulate started");
            InitParams();
            Console.WriteLine("Initialization is done");
            Console.WriteLine("Main update loop started");
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
            DbEntities = WebApiHelper.ExecuteWebApiRequest<List<ParamsEntity>>(SettingsApp.Default.DefaultServiceHost,
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
                    UpdateRandomEntity(EntityService.GetNewRandomEntity(), true);

                DbEntities = WebApiHelper.ExecuteWebApiRequest<List<ParamsEntity>>(SettingsApp.Default.DefaultServiceHost,
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
                    randomElemPos = EntityService.RandomGenerator.Next(DbEntities.Count);
                    isInList = updatedId.IndexOf(randomElemPos) != -1;
                }
                while (isInList);

                long oldId = DbEntities[randomElemPos].Id;
                DbEntities[randomElemPos] = EntityService.GetNewRandomEntity();
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
        private static bool UpdateRandomEntity(ParamsEntity entity, bool isCreate)
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
                Task<string> task = WebApiHelper.ExecuteWebApiRequest<string>(SettingsApp.Default.DefaultServiceHost, uri, _method, bodyPostData);
                task.Wait();

                return task.IsCompleted && !task.IsFaulted;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}