using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared
{
    public class ProductQueryParams
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public string? Search { get; set; }
        public ProductSortingOption Sort {  get; set; }
        
        // Skip
        private int _pageIndex = 1;
        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                //  عادي value  رجع ال ( 1)  لو اكبر من الصفر رجع ال value <= 0 لو 
                _pageIndex = (value <= 0)? 1 : value;
            }
        }


        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;

        private int _pageSize = DefaultPageSize; // = 5
        // Take
        public int PageSize
        {
            get 
            { 
                return _pageSize; 
            }
            set
            {
                if (value <= 0)
                    _pageIndex = DefaultPageSize;
                else if (value > MaxPageSize)
                    _pageSize = MaxPageSize;
                else 
                    _pageSize = value;
            }
        }
        

        


    }
}
