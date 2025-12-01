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
    public class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
     

        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(X => X.Price) // Price 
                   .HasPrecision(8, 2);

            builder.Property(X => X.ShortName).HasMaxLength(50); // ShortName

            builder.Property(X => X.Description).HasMaxLength(100); // Description

            builder.Property(X => X.DeliveryTime).HasMaxLength(50); // DeliveryTime
        }
    }
}
