using AutoMapper;
using AutoMapper.Execution;
using E_Commerce.Domain.Entities.OrderModule;
using E_Commerce.Shared.DTOs.OrderDTOs;
using Microsoft.Extensions.Configuration;

namespace E_Commerce.Services.MappingProfile
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver( IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            // 
            if(string.IsNullOrWhiteSpace(source.Product.PictureUrl)) 
                return string.Empty;

            if (source.Product.PictureUrl.StartsWith("http"))
                  return source.Product.PictureUrl;

            // معانا جاهز BaseUrl دا ال
            var BaseUrl = _configuration.GetSection("URLs")["BaseUrl"];
            if(string.IsNullOrEmpty(BaseUrl)) 
                return string.Empty;

            // لا انا بقي الي هخزنو بالطريقه دي http لو الصوره او اللينك الي جاي مش بيبدا ب
            // BaseUrl => https://localhost:7175/
            return $"{BaseUrl}{source.Product.PictureUrl}";
        }
    }
}