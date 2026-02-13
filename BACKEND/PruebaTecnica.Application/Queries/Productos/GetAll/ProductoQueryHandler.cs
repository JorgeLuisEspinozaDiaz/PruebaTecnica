using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PruebaTecnica.Application.IRepositories;
using PruebaTecnica.Application.Queries.Productos.Common;
using PruebaTecnica.Common.Core.DTO;
using PruebaTecnica.Common.Application.Helpers;

using Entities = PruebaTecnica.Core.Entities;

namespace PruebaTecnica.Application.Queries.Productos.GetAll
{
    public class ProductoQueryHandler : IRequestHandler<ProductoQuery, PaginatedResponseDTO>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly PageHelper _pageHelper = new PageHelper();

        public ProductoQueryHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<PaginatedResponseDTO> Handle(ProductoQuery request, CancellationToken cancellationToken)
        {
            var response = new PaginatedResponseDTO();
            
            try
            {
                Expression<Func<Entities.Producto, bool>> expression = x => x.Deleted == null 
                    && (string.IsNullOrEmpty(request.Search) || x.Nombre.Contains(request.Search))
                    && (!request.Id.HasValue || x.Id == request.Id.Value);

                var allData = await _productoRepository.GetAsync(expression);
                var total = allData?.Count ?? 0;
                
                var paginatedResult = allData?.Skip(request.Skip).Take(request.Take).ToList();

                var paginatedData = paginatedResult?.Select(x => new ProductoDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Precio = x.Precio,
                    FechaCreacion = x.FechaCreacion
                }).ToList();

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Confirmacion = true;
                response.Mensaje = "Consulta exitosa";
                response.Data = paginatedData;
                response.Total = total;
                response.Pages = _pageHelper.calcularPages(request.Take, total);
                response.Page = (request.Skip / request.Take) + 1;
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Confirmacion = false;
                response.Mensaje = $"Error al obtener los productos: {ex.Message}";
            }

            return response;
        }
    }
}
