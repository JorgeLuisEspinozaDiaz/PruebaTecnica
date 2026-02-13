using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PruebaTecnica.Common.Core.Base;

namespace PruebaTecnica.Core.Entities
{
    public class Producto : EntityBase
    {
        public Producto()
        {
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
         public decimal Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
     }
}
