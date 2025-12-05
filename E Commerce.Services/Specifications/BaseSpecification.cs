using E_Commerce.Domain.Entites;
using E_Commerce.Domain.Intreface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    public abstract class BaseSpecification<TEntity , TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        #region Criteria Or Where 

        public Expression<Func<TEntity, bool>> Criteria { get; }
        // where عشان اجبر كلو يستخدم ال constractor عملتها جوا
        protected BaseSpecification(Expression<Func<TEntity, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }

        #endregion


        #region Includes

        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        // BaseSpecification دي اللي هيستخدمهم بس هو اللي هيكون وارث من ال
        protected void AddInclude(Expression<Func<TEntity, object>> includeExp)
        {
            IncludeExpressions.Add(includeExp);
        }

        #endregion


        #region Sorting [ OrderBy - OrderByDescending ]

        public Expression<Func<TEntity, object>> OrderBy {  get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending {  get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> OrderByDescendingExpression)
        {
            OrderByDescending = OrderByDescendingExpression;
        }



        #endregion


        #region Pagination [ Take - Skip ]

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPaginated { get; private set; }

        // Total 40 
        // pagesize = 10   كل صفحه فيها 10 
        // 10 , 10 , 10 , 10 
        // pageIndex = 3 => كدا انا عاوز اوصل ل تالت 10 عندي 
        protected void ApplyPagination(int pageSize , int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            //   ( 3 - 1 = 2 ) => ( 2 * 10 ) = 20 =>  رقم 3 pageIndex    ل اول  20  عندي و هيوصل لSkip في الحاله دي هيعمل
            Skip = (pageIndex - 1) * pageSize;

        }

        #endregion

    }
}
