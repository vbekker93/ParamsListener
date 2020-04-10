using Infrastructure.Models;
using System;

namespace ExternalAPIEmulator
{
    public static class EntityService
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Генератор случайных величин
        /// </summary>
        public static Random RandomGenerator
        {
            get
            {
                return random;
            }
        }

        /// <summary>
        /// Получить объект сущности со случайными значениями параметров
        /// </summary>
        /// <returns>Объект сущности</returns>
        public static ParamsEntity GetNewRandomEntity()
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
        /// <returns>Случайное дробное число</returns>
        public static double GetRandomNumber(double minimum, double maximum)
        {
            return Math.Round(EntityService.RandomGenerator.NextDouble() * (maximum - minimum) + minimum, 2);
        }
    }
}