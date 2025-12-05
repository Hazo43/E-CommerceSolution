using E_Commerce.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.OrderModule
{
    public class OrderItem : BaseEntity<int>
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; } 
        public ProductItemOrdered Product { get; set; } = default!;
        
        // Order -> 1  , OrderItem -> M
        public Order Order { get; set; } = default!;
        public int OrderId { get; set; }

    }
}
