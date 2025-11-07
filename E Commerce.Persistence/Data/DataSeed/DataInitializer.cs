using E_Commerce.Domain.Entites;
using E_Commerce.Domain.Entites.ProductModule;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Domain.Interface;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeed
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext dbContext;

        public DataInitializer( StoreDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task InitializeAsync()
        {
            try
            {

                var HasBrands = await dbContext.ProductBrands.AnyAsync();
                var HasTypes = await dbContext.ProductTypes.AnyAsync();
                var HasProducs = await dbContext.Products.AnyAsync();

                /// لو فيهم داتا اطلع متكملش
                if (HasBrands && HasTypes && HasProducs) return;

                // لو مفهمش بقا روح ضيف
                if (!HasBrands)
                   await DataSeedFromJSONAsync<ProductBrand, int>("brands.json", dbContext.ProductBrands);

                if (!HasTypes)
                  await  DataSeedFromJSONAsync<ProductType, int>("types.json", dbContext.ProductTypes);

                await dbContext.SaveChangesAsync();

                if (!HasProducs)
                   await DataSeedFromJSONAsync<Product, int>("products.json", dbContext.Products);
                
                await dbContext.SaveChangesAsync();

            }
            catch (Exception ex) 
            {
                Console.WriteLine($" Data Seeding Failed : {ex}");
            }

        }



        private async Task DataSeedFromJSONAsync<T , TKey>(string fileName , DbSet<T> dbset) where T : BaseEntity<TKey>
        {

            // D:\Poute .NET\08 - API\Session 02\E-CommerceSolution\E Commerce.Persistence\Data\DataSeed\JSONFiles\products.json

            var filePath = @"..\E Commerce.Persistence\Data\DataSeed\JSONFiles\" + fileName;

            // throw لو مش موجود رجع ال
            if (!File.Exists(filePath)) 
                throw new FileNotFoundException($"File {fileName} Is Not Exists");

            try
            {
                using var dataStream = File.OpenRead(filePath);

                var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream , new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

                if(data is not null)
                {
                   await dbset.AddRangeAsync(data);
                }

            }
            catch (Exception ex) 
            {
                Console.WriteLine($" Error While Reading JSON File : {ex}");
            }

        }



    }

}
