using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException( string message) : base(message)
        {
            
        }
    }

    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException( int id) : base($" Product With Id {id} is Not Found ")
        {
            
        }
    }

    public sealed class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string id) : base($" Basket With Id {id} is Not Found ")
        {

        }
    }
}
