using System;

namespace PruebaTecnica.Application.Queries.Productos.Common
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
