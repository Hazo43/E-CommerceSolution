using AutoMapper;
using E_Commerce.Domain.Entities.BasketModule;
using E_Commerce.Domain.Entities.OrderModule;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Domain.Intreface;
using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.OrderDTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public OrderService( IMapper mapper , IUnitOfWork unitOfWork , IBasketRepository basketRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }
        public async Task<Result<OrderToReturnDTO>> CreateOrderAsync(OrderDTO orderDTO, string Email)
        {

            //Maps the provided shipping address to the order address entity.
            var OrderAddress =  _mapper.Map<AddressDTO ,OrderAddress>(orderDTO.Address);  // OrderAddress عشان نجيب ال

            //Retrieves the basket and validates its existence.
            var Basket = await _basketRepository.GetBasketAsync(orderDTO.BasketId);
            if (Basket is null)
                return Error.NotFound("Basket.NotFound" , $"The Basket With Id {orderDTO.BasketId} Is Not Found ");

            // Creates a list of order items by fetching product details from the database and validating each product.
            List<OrderItem> orderitems = new List<OrderItem>();
            
            foreach(var item in Basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id); 
                if(product is null)
                    return Error.NotFound("Product.NotFound", $"The Product With Id {item.Id} Is Not Found ");

                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrdered { ProductId =  product.Id , PictureUrl = product.PictureUrl , ProductName = product.Name },
                    Price = item.Price,
                    Quantity = item.Quantity,
                };
              
                orderitems.Add(orderItem);
            }
           
            // Retrieves the selected delivery method and validates its existence
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDTO.DeliveryMethodId);
            if(DeliveryMethod is null)
                return Error.NotFound("DeliveryMethod.NotFound", $"The Delivery Method With Id {orderDTO.DeliveryMethodId} Is Not Found ");

            // Calculates the subtotal of the order based on the items and their quantities.
            var SubTotal = orderitems.Sum( I => I.Price * I.Quantity );

            // Creates a new Order with all relevant details.
            var order = new Order()
            {
                Address = OrderAddress,
                DeliveryMethod = DeliveryMethod,
                Items = orderitems,
                SubTotal = SubTotal,
                UserEmail = Email,
            };
            
            await _unitOfWork.GetRepository<Order , Guid>().AddAsync(order);
            //
            int Result = await _unitOfWork.SaveChangesAsync();
            if (Result == 0)
                Error.Failure("Order.Failure" , " Order Can Not Be Created");

            //Returns a DTO containing the full order details to the client, including Id [OrderId] , UserEmail ,  items [ProductName , PictureUrl , Price , Quantity], address, delivery method [ShortName], order status, OrderDate , subtotal, and total price
            return _mapper.Map<Order , OrderToReturnDTO>(order);

        }
    }
}
