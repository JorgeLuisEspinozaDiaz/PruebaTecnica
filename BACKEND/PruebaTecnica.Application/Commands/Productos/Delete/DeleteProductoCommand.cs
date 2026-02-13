using System;
using MediatR;
using PruebaTecnica.Common.Core.DTO;

namespace PruebaTecnica.Application.Commands.Productos.Delete
{
    public class DeleteProductoCommand : IRequest<BaseResponseDTO>
    {
        public int Id { get; set; }
    }
}
