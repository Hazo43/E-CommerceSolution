using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    internal class ProductWithTypeAndBrandSpecifications : BaseSpecification<Product , int>
    {
        // Get Product By Id 
        public ProductWithTypeAndBrandSpecifications( int id) : base( P => P.Id == id ) 
        {
            AddInclude(P => P.productType);
            AddInclude(P => P.ProductBrand);
        }


        // Get All Product 
        public ProductWithTypeAndBrandSpecifications(ProductQueryParams queryParams)
            : base(ProductSpecificationsHelper.GetProductCriteria(queryParams))


        {
            AddInclude(P => P.productType);
            AddInclude(P => P.ProductBrand);

            switch(queryParams.Sort)
            {
                case ProductSortingOption.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOption.NameDec:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortingOption.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOption.PriceDec:
                    AddOrderByDescending(P => P.Price);
                    break;
                default: AddOrderBy(P => P.Id);
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }
    }
}
