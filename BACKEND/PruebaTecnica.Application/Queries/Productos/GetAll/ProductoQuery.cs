using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PruebaTecnica.Common.Core.DTO;
using PruebaTecnica.Common.Core.Filter;

namespace PruebaTecnica.Application.Queries.Productos.GetAll
{
    public class ProductoQuery : BaseFilter, IRequest<PaginatedResponseDTO>
    {
    }
}
