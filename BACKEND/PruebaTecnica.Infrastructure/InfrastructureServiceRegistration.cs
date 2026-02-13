using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaTecnica.Common.Application;
using PruebaTecnica.Application.IRepositories;
using PruebaTecnica.Infrastructure.Repositories;

namespace PruebaTecnica.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PruebaTecnicaDbContext>(opts =>
                opts.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsHistoryTable("__EFMigrationHistory", "dbo")
                )
            );

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            // Repositories
            services.AddScoped<IProductoRepository, ProductoRepository>();

            return services;
        }
    }
}
