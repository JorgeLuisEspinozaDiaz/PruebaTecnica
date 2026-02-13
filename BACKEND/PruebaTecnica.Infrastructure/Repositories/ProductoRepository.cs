using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PruebaTecnica.Application.IRepositories;
using PruebaTecnica.Core.Entities;

namespace PruebaTecnica.Infrastructure.Repositories
{
    public class ProductoRepository : RepositoryBase<Producto>, IProductoRepository
    {
        public ProductoRepository(PruebaTecnicaDbContext dbContext) : base(dbContext)
        {
        }
    }
}
