
using E_Commerce.Domain.Interface;
using E_Commerce.Domain.Intreface;
using E_Commerce.Persistence.Data.DataSeed;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.UnitOfWork;
using E_Commerce.Services;
using E_Commerce.Services.MappingProfile;
using E_Commerce.Services_Abstraction;
using E_Commerce.Wep.Extentions;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace E_Commerce.Wep
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region  Add services to the container

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // Data Seed
            builder.Services.AddScoped<IDataInitializer ,  DataInitializer>();
            builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();
            // Mapper
            //ProductProfile => mappingProfile «··Ì «‰« ⁄«„·Ê ⁄‘«‰ ﬂœ« ÂÊ ÂÌ‘Ê› ﬂ· Õ«ÃÂ ›Ì «· mappingProfile ›Ì «· class ·«“„ ÌﬂÊ‰              
            builder.Services.AddAutoMapper( typeof(ProductProfile).Assembly);
            builder.Services.AddScoped<IProductService, ProductService>();
            #endregion


            var app = builder.Build();

            #region Data Seeding

           await app.MigrateDatabaseAsync();

           await app.SeedDatabaseAsync();

            //using var Scop = app.Services.CreateScope();
            //// Check Migrations 
            //var dbContextServices = Scop.ServiceProvider.GetRequiredService<StoreDbContext>();
            //// „‘ „⁄„Ê·Â ÂÌ—ÊÕ Ì⁄„·Â« ﬁ»· „« ÌÕ›Ÿ «·œ« « Migrations ·Ê ›ÌÂ «Ì 
            //if (dbContextServices.Database.GetPendingMigrations().Any())
            //    dbContextServices.Database.Migrate();

            ////  Data Seed 
            //var DataInitializerService = Scop.ServiceProvider.GetRequiredService<IDataInitializer>();
            //DataInitializerService.Initialize();

            #endregion

            #region Configure the HTTP request pipeline.


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            //
            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            #endregion

           await app.RunAsync();
       
        }
    }
}
