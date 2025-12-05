using E_Commerce.Domain.Entites;
using E_Commerce.Domain.Intreface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence
{
    internal static class SpecificationsEvaluator
    {

        // Create Query - Build Query 

        // اللي احنا عاوزين نكونها Query دا ال
        // dbcontext.Products.Include(P => P.ProductType).Include(P => P.ProductBrand);

        public static IQueryable<TEntity> CreateQuery<TEntity , TKey>(IQueryable<TEntity> EntryPoint ,
             ISpecifications<TEntity , TKey> specifications)  where TEntity : BaseEntity<TKey>
        {

            // Query بدايه ال
            var Query = EntryPoint;  // // dbcontext.Products

            if(specifications is not null)
            {
                // Where 
                if (specifications.Criteria is not null)
                {
                    // Query =>  dbcontext.Products.Where()
                    Query = Query.Where(specifications.Criteria);
                }

                // OrderBy 
                if (specifications.OrderBy is not null)
                {
                    Query = Query.OrderBy(specifications.OrderBy);
                }

                // OrderBySescending 
                if (specifications.OrderByDescending is not null)
                {
                    Query = Query.OrderBy(specifications.OrderByDescending);
                }

                // Include 
                // و لو فيه اي عنصر روح اعمل اللي بقولك عليه null لو مش ب 
                if (specifications.IncludeExpressions  is not null && specifications.IncludeExpressions.Any())
                {

                    


                    // Aggregate =>  بتاعها و تكمل عليهout put و بتاخد ال Collection بتشتغل علي  Aggregate
                    Query = specifications.IncludeExpressions.Aggregate(Query,
                         (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));

                    // dbcontext.Products بدانا ب
                    // .Include(P => P.ProductType)  اللي هو دا IncludeExp اول 

                    // CurrentQuery دي كلها ال
                    // dbcontext.Products.Include(P => P.ProductType) 

                    //  التاني  اللي تحتي داExpression لل Include وبعدين يبدا يعمل
                    // .Include(P => P.ProductBrand) 
                    //  dbcontext.Products.Include(P => P.ProductType).Include(P => P.ProductBrand) 
                }

                // Take - Skip
                if(specifications.IsPaginated == true)
                {
                    Query = Query.Skip(specifications.Skip).Take(specifications.Take);
                }
            
            }


            //Query دي نهايه ال
            return Query;

        }
    }
}
