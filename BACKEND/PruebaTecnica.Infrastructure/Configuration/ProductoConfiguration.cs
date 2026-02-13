using Microsoft.EntityFrameworkCore;
 using PruebaTecnica.Core.Entities;
 using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PruebaTecnica.Infrastructure.Configuration
{
    public class ProductoConfiguration 
    {
        public ProductoConfiguration(EntityTypeBuilder<Producto> entityTypeBuilder)
        {
             entityTypeBuilder.HasKey(x => x.Id);

     
             entityTypeBuilder.Property(x => x.Nombre)
                 .HasMaxLength(100)
                .IsRequired();

            // Precio (decimal(18,2))
            entityTypeBuilder.Property(x => x.Precio)
                .HasPrecision(18, 2)
                .IsRequired();

            // FechaCreacion (datetime)
            entityTypeBuilder.Property(x => x.FechaCreacion)
                 .IsRequired();
 
         }
    }
}