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
    public class OrderConfigrations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // SubTotal 
            builder.Property(X => X.SubTotal)
                   .HasPrecision(8, 2);

            // Owend Address
            builder.OwnsOne(X => X.Address, OwndEntity =>
            {
                OwndEntity.Property( X => X.FirstName).HasMaxLength(50);
                OwndEntity.Property( X => X.LastName).HasMaxLength(50);
                OwndEntity.Property( X => X.City).HasMaxLength(50);
                OwndEntity.Property( X => X.Street).HasMaxLength(50);
                OwndEntity.Property( X => X.Country).HasMaxLength(50);
            });
        }
    }
}
