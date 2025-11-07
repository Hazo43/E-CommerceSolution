using E_Commerce.Domain.Entites;
using E_Commerce.Domain.Intreface;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class GenericRepository<TEntitty, TKey> : IGenericRepository<TEntitty, TKey> where TEntitty : BaseEntity<TKey>
    {
        private readonly StoreDbContext dbContext;

        public GenericRepository( StoreDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task AddAsync(TEntitty entity)
               => await dbContext.Set<TEntitty>().AddAsync(entity);

        public async Task<IEnumerable<TEntitty>> GetAllAsync()
                      => await dbContext.Set<TEntitty>().ToListAsync();

        public async Task<TEntitty> GetByIdAsync(TKey id)
                => await dbContext.Set<TEntitty>().FindAsync(id);

        public void Remove(TEntitty entity)
                 => dbContext.Set<TEntitty>().Remove(entity);

        public void Update(TEntitty entity)
               => dbContext.Set<TEntitty>().Update(entity);
    }
}
