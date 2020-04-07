using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace ParamsListenerUpdateService.Contexts
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