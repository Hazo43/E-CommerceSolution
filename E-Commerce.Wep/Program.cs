
using E_Commerce.Domain.Entities.IdentityModule;
using E_Commerce.Domain.Interface;
using E_Commerce.Domain.Intreface;
using E_Commerce.Persistence.Data.DataSeed;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.IdentityData.DataSeed;
using E_Commerce.Persistence.IdentityData.DbContext;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Persistence.UnitOfWork;
using E_Commerce.Services;
using E_Commerce.Services.MappingProfile;
using E_Commerce.Services_Abstraction;
using E_Commerce.Wep.CustomMiddleWares;
using E_Commerce.Wep.Extentions;
using E_Commerce.Wep.Factories;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StackExchange.Redis;
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
            // Data Seed  ///////////////////////////////////// 
            builder.Services.AddKeyedScoped<IDataInitializer ,  DataInitializer>("Default");
            builder.Services.AddKeyedScoped<IDataInitializer, IdentityDataInitializer>("Identity");
            builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();
            // Mapper
            //ProductProfile => mappingProfile «··Ì «‰« ⁄«„·Ê ⁄‘«‰ ﬂœ« ÂÊ ÂÌ‘Ê› ﬂ· Õ«ÃÂ ›Ì «· mappingProfile ›Ì «· class ·«“„ ÌﬂÊ‰              
            builder.Services.AddAutoMapper( typeof(ProductProfile).Assembly);
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddSingleton<IConnectionMultiplexer>(SP =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            });
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<IBasketService , BasketService>();
            builder.Services.AddScoped<ICacheRepository, CacheRepository>();
            builder.Services.AddScoped<ICacheService, CacheService>();
          
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse;
            });
            // 
            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
             // 
            builder.Services.AddIdentityCore<ApplicationUser>()
                            .AddRoles<IdentityRole>()
                            .AddEntityFrameworkStores<StoreIdentityDbContext>();

            builder.Services.AddScoped<IAuthenticationService , AuthenticationService>();
            #endregion


            var app = builder.Build();

            #region Data Seeding

           await app.MigrateDatabaseAsync();
           await app.MigrateIdentityDatabaseAsync();
           await app.SeedDatabaseAsync();
           await app.SeedIdentityDatabaseAsync();

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

            app.UseMiddleware<ExceptionHandlerMiddleWare>();

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
