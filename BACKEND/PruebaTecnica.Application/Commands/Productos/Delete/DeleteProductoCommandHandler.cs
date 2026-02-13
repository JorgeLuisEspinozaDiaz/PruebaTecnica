using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PruebaTecnica.Application.IRepositories;
using PruebaTecnica.Common.Core.DTO;

namespace PruebaTecnica.Application.Commands.Productos.Delete
{
    public class DeleteProductoCommandHandler : IRequestHandler<DeleteProductoCommand, BaseResponseDTO>
    {
        private readonly IProductoRepository _productoRepository;

        public DeleteProductoCommandHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<BaseResponseDTO> Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
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

                // Soft delete
                producto.Deleted = DateTime.Now;
                await _productoRepository.UpdateAsync(producto);

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Confirmacion = true;
                response.Mensaje = "Producto eliminado exitosamente";
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Confirmacion = false;
                response.Mensaje = $"Error al eliminar el producto: {ex.Message}";
            }

            return response;
        }
    }
}
