using E_Commerce.Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.Configrations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Price 
            builder.Property(X => X.Price)
                   .HasPrecision(8, 2);

            // Order Item Own Product Order Item
            builder.OwnsOne(X => X.Product, owned =>
            {
                owned.Property(X => X.ProductName).HasMaxLength(100);
                owned.Property(X => X.PictureUrl).HasMaxLength(200);
            });
        }
    }
}
