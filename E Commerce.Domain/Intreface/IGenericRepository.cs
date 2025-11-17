using E_Commerce.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Intreface
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity , TKey> specifications);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity> GetByIdAsync(ISpecifications<TEntity , TKey> specifications);
        Task AddAsync(TEntity entity);
        void Remove (TEntity entity);
        void Update(TEntity entity);
        Task<int> CountAysnc( ISpecifications<TEntity, TKey> specifications );


    }
}
