using System;
using MediatR;
using PruebaTecnica.Common.Core.DTO;

namespace PruebaTecnica.Application.Commands.Productos.Update
{
    public class UpdateProductoCommand : IRequest<BaseResponseDTO>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }
}
