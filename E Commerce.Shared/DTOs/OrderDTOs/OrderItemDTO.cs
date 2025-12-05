using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DTOs.OrderDTOs
{
    public record OrderItemDTO
    (
      string ProductName , 
      string PictureUrl , 
      decimal Price , 
      int Quantity 
    );
}
