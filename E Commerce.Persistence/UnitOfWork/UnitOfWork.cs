using E_Commerce.Domain.Entites;
using E_Commerce.Domain.Intreface;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.UnitOfWork
{
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext dbContext;
        private readonly Dictionary<Type, object> _Repository = [];
        public UnitOfWork( StoreDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var EntityType = typeof(TEntity);

            // repository رجعو في ال EntityType لو لقيت ال
            if (_Repository.TryGetValue(EntityType , out object? repository))
                return (IGenericRepository<TEntity, TKey>) repository;

            // جديده repo لو ملقاش بقا؟؟ هنعمل 
            var newRepo = new GenericRepository<TEntity, TKey>(dbContext);
            _Repository[EntityType] = newRepo;
            return newRepo;

        }

        public async Task<int> SaveChangesAsync()
             => await dbContext.SaveChangesAsync();
    }
}
