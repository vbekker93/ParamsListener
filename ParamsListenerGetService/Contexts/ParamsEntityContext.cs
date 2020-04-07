using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ParamsListenerGetService.Contexts
{
    public class ParamsEntityContext : DbContext
    {
        public ParamsEntityContext(DbContextOptions<ParamsEntityContext> options)
            : base(options)
        {
            /// Создание базы, если не инициализирована (Code-First)
            Database.EnsureCreated();
        }

        public DbSet<ParamsEntity> ParamsEntities { get; set; }
    }
}