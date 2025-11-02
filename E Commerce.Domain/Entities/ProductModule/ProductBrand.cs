using E_Commerce.Domain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entites.ProductModule
{
    public class ProductBrand : BaseEntity<int>
    {
        public string Name { get; set; } = default!;

        #region RelathioShip

        // Product (m) -- Brand (1)

        public ICollection<Product> Products { get; set; }

        #endregion
    }
}
