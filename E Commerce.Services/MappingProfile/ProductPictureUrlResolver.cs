using AutoMapper;
using AutoMapper.Execution;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.MappingProfile
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureUrlResolver( IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            
            // فاضيه string  رجعلو Empty او Null لو الصوره ب
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;

            // يبقي كدا هو جاي جاهز نرجعهولو زي ما هو عادي http  لو لينك الصوره جاي ب
            if (source.PictureUrl.StartsWith("http"))
                return source.PictureUrl;

            // Key دا اللي شايل اللينك نفسو دا ال BaseUrl و  AppSsttings دا انا معرفو في ال URLs ال 
            //  بتاعي api بتاع البرنامج بتاعي او بتاع ال https://localhost:7175/ شايل BaseUrl كدا ال
            var BaseUrl = configuration.GetSection("URLs")["BaseUrl"];
            // فاضيه string هرجع null هعمل اتشك لو ب 
            if (BaseUrl == null)
                return string.Empty;

            // لا انا بقي الي هخزنو بالطريقه دي http لو الصوره او اللينك الي جاي مش بيبدا ب
            // BaseUrl => https://localhost:7175/   , source.PictureUrl دي علي حسب امتداد الصوره   
            var picUrl = $"{BaseUrl}{source.PictureUrl}";

            return picUrl;
        }
    }
}
