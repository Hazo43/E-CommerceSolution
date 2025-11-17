using E_Commerce.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Intreface
{
    public interface ISpecifications<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity , object>>> IncludeExpressions { get;  } // Include

        public Expression<Func<TEntity , bool>> Criteria {  get; } // Where 

        public Expression<Func<TEntity , object>> OrderBy { get; } // OrderBy

        public Expression<Func<TEntity , object>> OrderByDescending { get; } // OrderByDescending

        public int Take { get; } // Take 
        public int Skip { get; } // Skip 
        public bool IsPaginated {  get; } // ولا لا Pagination هو عمل Check عشان نعمل
    }
}
