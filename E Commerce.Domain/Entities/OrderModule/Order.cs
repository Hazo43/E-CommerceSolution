using E_Commerce.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.OrderModule
{
    public class Order : BaseEntity<Guid>
    {

        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } =DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public OrderAddress Address { get; set; } = default!;
     
        // DeliveryMethod -> M , Order -> 1
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliverMethodId { get; set; } // FK   By Convention هيفهمو

        // Order -> 1  , OrderItem -> M
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; } // Total Price Of Item 
        public  decimal GetTotal() => SubTotal + DeliveryMethod.Price; // Method 
    
    } 
}
