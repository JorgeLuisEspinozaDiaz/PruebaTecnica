using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PruebaTecnica.Application.IRepositories;
using PruebaTecnica.Common.Core.DTO;

namespace PruebaTecnica.Application.Commands.Productos.Update
{
    public class UpdateProductoCommandHandler : IRequestHandler<UpdateProductoCommand, BaseResponseDTO>
    {
        private readonly IProductoRepository _productoRepository;

        public UpdateProductoCommandHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<BaseResponseDTO> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseDTO();

            try
            {
                var producto = await _productoRepository.GetEntityAsync(x => x.Id == request.Id && x.Deleted == null);

                if (producto == null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Confirmacion = false;
                    response.Mensaje = "Producto no encontrado";
                    return response;
                }

                producto.Nombre = request.Nombre;
                producto.Precio = request.Precio;

                await _productoRepository.UpdateAsync(producto);

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Confirmacion = true;
                response.Mensaje = "Producto actualizado exitosamente";
                response.Data = null;
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Confirmacion = false;
                response.Mensaje = $"Error al actualizar el producto: {ex.Message}";
            }

            return response;
        }
    }
}
