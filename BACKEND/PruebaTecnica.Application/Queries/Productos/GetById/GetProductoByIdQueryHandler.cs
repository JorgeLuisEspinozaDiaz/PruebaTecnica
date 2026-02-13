using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PruebaTecnica.Application.IRepositories;
using PruebaTecnica.Application.Queries.Productos.Common;

namespace PruebaTecnica.Application.Queries.Productos.GetById
{
    public class GetProductoByIdQueryHandler : IRequestHandler<GetProductoByIdQuery, ProductoDTO>
    {
        private readonly IProductoRepository _productoRepository;

        public GetProductoByIdQueryHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<ProductoDTO> Handle(GetProductoByIdQuery request, CancellationToken cancellationToken)
        {
            var producto = await _productoRepository.GetEntityAsync(x => x.Id == request.Id && x.Deleted == null);
            
            if (producto == null)
                return null;

            return new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                FechaCreacion = producto.FechaCreacion
            };
        }
    }
}
