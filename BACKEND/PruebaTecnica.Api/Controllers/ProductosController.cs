using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PruebaTecnica.Application.Commands.Productos.Create;
using PruebaTecnica.Application.Commands.Productos.Update;
using PruebaTecnica.Application.Commands.Productos.Delete;
using PruebaTecnica.Application.Queries.Productos.GetAll;
using PruebaTecnica.Application.Queries.Productos.GetById;
using PruebaTecnica.Application.Queries.Productos.Common;
using PruebaTecnica.Common.Core.DTO;

namespace PruebaTecnica.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ILogger<ProductosController> _logger;
        private readonly IMediator _mediator;

        public ProductosController(ILogger<ProductosController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene todos los productos con paginación
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedResponseDTO>> ObtenerTodos([FromQuery] ProductoQuery query)
        {
            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Obtiene un producto por su Id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductoDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductoDTO>> ObtenerPorId(int id)
        {
            var result = await _mediator.Send(new GetProductoByIdQuery { Id = id });
            
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo producto
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BaseResponseDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BaseResponseDTO>> Crear([FromBody] CreateProductoCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Actualiza un producto existente
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponseDTO), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponseDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BaseResponseDTO>> Actualizar([FromBody] UpdateProductoCommand command)
        {
             var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Elimina un producto (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BaseResponseDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponseDTO), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BaseResponseDTO>> Eliminar(int id)
        {
            var result = await _mediator.Send(new DeleteProductoCommand { Id = id });
            return StatusCode(result.StatusCode, result);
        }
    }
}
