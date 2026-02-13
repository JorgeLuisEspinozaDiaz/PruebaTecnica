using System;
using MediatR;
using PruebaTecnica.Common.Core.DTO;

namespace PruebaTecnica.Application.Commands.Productos.Create
{
    public class CreateProductoCommand : IRequest<BaseResponseDTO>
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }
}
