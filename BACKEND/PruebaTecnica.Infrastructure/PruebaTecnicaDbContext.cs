using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Common.Application.Helpers;
using PruebaTecnica.Common.Core.Base;
using PruebaTecnica.Core.Entities;
using PruebaTecnica.Infrastructure.Configuration;


namespace PruebaTecnica.Infrastructure
{
    public class PruebaTecnicaDbContext : DbContext
    {
        private readonly DateTimeHelper _helper = new DateTimeHelper();

        public PruebaTecnicaDbContext(DbContextOptions<PruebaTecnicaDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");
            ModelConfig(modelBuilder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {

            new ProductoConfiguration(modelBuilder.Entity<Producto>());



            modelBuilder.Entity<Producto>().ToTable("Productos");
         }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _helper.DateTimePst();
                        entry.Entity.Modified = _helper.DateTimePst();
                        break;
                    case EntityState.Modified:
                        entry.Entity.Modified = _helper.DateTimePst();
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
