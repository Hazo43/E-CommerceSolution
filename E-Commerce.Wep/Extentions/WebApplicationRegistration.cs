using E_Commerce.Domain.Interface;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.IdentityData.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce.Wep.Extentions
{
    public static class WebApplicationRegistration
    {
        public static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
        {
           await using var Scop = app.Services.CreateAsyncScope();
            // Check Migrations 
            var dbContextServices = Scop.ServiceProvider.GetRequiredService<StoreDbContext>();
            // مش معموله هيروح يعملها قبل ما يحفظ الداتا Migrations لو فيه اي 
            var PendingMigrations = await dbContextServices.Database.GetAppliedMigrationsAsync();
            if (PendingMigrations.Any())
               await dbContextServices.Database.MigrateAsync();

            return app;
        }
        public static async Task<WebApplication> MigrateIdentityDatabaseAsync(this WebApplication app)
        {
           await using var Scop = app.Services.CreateAsyncScope();
            // Check Migrations 
            var dbContextServices = Scop.ServiceProvider.GetRequiredService<StoreIdentityDbContext>();
            // مش معموله هيروح يعملها قبل ما يحفظ الداتا Migrations لو فيه اي 
            var PendingMigrations = await dbContextServices.Database.GetAppliedMigrationsAsync();
            if (PendingMigrations.Any())
               await dbContextServices.Database.MigrateAsync();

            return app;
        }

        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
           await using var Scop = app.Services.CreateAsyncScope();
            var DataInitializerService = Scop.ServiceProvider.GetRequiredKeyedService<IDataInitializer>("Default");
           await DataInitializerService.InitializeAsync();

            return app;
        }
        public static async Task<WebApplication> SeedIdentityDatabaseAsync(this WebApplication app)
        {
           await using var Scop = app.Services.CreateAsyncScope();
            var DataInitializerService = Scop.ServiceProvider.GetRequiredKeyedService<IDataInitializer>("Identity");
           await DataInitializerService.InitializeAsync();

            return app;
        }

    }
}
