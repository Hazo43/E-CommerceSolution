using E_Commerce.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.Configrations
{
    public class ProducConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey( X => X.Id );

            builder.Property(x => x.Name)
                   .HasMaxLength(100);

            builder.Property(x => x.Description)
                   .HasMaxLength(500);

            builder.Property(x => x.PictureUrl)
                   .HasMaxLength(200);

            builder.Property(x => x.Price)
                   .HasPrecision(18, 2);

            // Brand
            builder.HasOne(x => x.ProductBrand)
                   .WithMany(x => x.Products)
                   .HasForeignKey(x => x.BrandId);
           
            // Type
            builder.HasOne(x => x.productType)
                   .WithMany(x => x.Products)
                   .HasForeignKey(x => x.TypeId);
        }
    }
}
