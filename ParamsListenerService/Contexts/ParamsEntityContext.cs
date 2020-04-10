using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ParamsListenerService.Contexts
{
    /// <summary>
    /// Контекст данных сущности
    /// </summary>
    public class ParamsEntityContext : DbContext
    {
        public ParamsEntityContext(DbContextOptions<ParamsEntityContext> options)
            : base(options)
        {
            /// Создание базы, если не инициализирована (Code-First)
            Database.EnsureCreated();
        }

        /// <summary>
        /// Представление сущностей в БД
        /// </summary>
        public DbSet<ParamsEntity> ParamsEntities { get; set; }
    }
}