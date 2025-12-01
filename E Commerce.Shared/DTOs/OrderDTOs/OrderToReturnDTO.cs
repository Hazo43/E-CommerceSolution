using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.OrderDTOs
{
    public record OrderToReturnDTO
        (
        Guid Id , // OrderId 
        string UserEmail ,
        ICollection<OrderItemDTO> Items , 
        AddressDTO Address,
        string DeliveryMethod ,
        string OrderStatus , 
        DateTimeOffset OrderDate,
        decimal Price,
        decimal SubTotal
        );
    
}
