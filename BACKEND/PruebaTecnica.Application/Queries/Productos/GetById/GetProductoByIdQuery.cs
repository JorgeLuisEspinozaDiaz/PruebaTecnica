using System;
using MediatR;
using PruebaTecnica.Application.Queries.Productos.Common;

namespace PruebaTecnica.Application.Queries.Productos.GetById
{
    public class GetProductoByIdQuery : IRequest<ProductoDTO>
    {
        public int Id { get; set; }
    }
}
