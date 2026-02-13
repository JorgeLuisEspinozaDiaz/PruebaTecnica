using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PruebaTecnica.Application.IRepositories;
using PruebaTecnica.Common.Core.DTO;
using PruebaTecnica.Core.Entities;

namespace PruebaTecnica.Application.Commands.Productos.Create
{
    public class CreateProductoCommandHandler : IRequestHandler<CreateProductoCommand, BaseResponseDTO>
    {
        private readonly IProductoRepository _productoRepository;

        public CreateProductoCommandHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<BaseResponseDTO> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseDTO();

            try
            {
                var producto = new Producto
                {
                    Nombre = request.Nombre,
                    Precio = request.Precio,
                    FechaCreacion = DateTime.Now
                };

                var result = await _productoRepository.AddAsync(producto);

                response.StatusCode = (int)HttpStatusCode.Created;
                response.Confirmacion = true;
                response.Mensaje = "Producto creado exitosamente";
                response.Data = result.Id;
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Confirmacion = false;
                response.Mensaje = $"Error al crear el producto: {ex.Message}";
            }

            return response;
        }
    }
}
