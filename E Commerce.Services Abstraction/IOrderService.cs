using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services_Abstraction
{
    public interface IOrderService
    {
        // Create Order 
        // 1. Take( OrderDTO , Email) => Return ( OrderToReturnDTO)

        Task<Result<OrderToReturnDTO>> CreateOrderAsync(OrderDTO orderDTO, string Email);
    }
}
