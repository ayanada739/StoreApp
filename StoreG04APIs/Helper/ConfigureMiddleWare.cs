using Store.G04.APIs.MiddleWares;
using Store.G04.Repository.Data.Contexts;
using Store.G04.Repository.Data;
using StoreG04APIs;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Store.G04.Repository.Identity.Contexts;
using Store.G04.Repository.Identity.DataSeed;
using Microsoft.AspNetCore.Identity;
using Store.G04.Core.Entities.Identity;

namespace Store.G04.APIs.Helper
{
    public static class ConfigureMiddleWare 
    {
        public static async Task<WebApplication> ConfigureMiddleWaresAsync(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<StoreDbContext>(); //Ask CLR Create Obj StoredDbContext
            var identityContext = services.GetRequiredService<StoreIdentityDbContext>(); //Ask CLR Create Obj StoreIdentityDbContext
            var userManager = services.GetRequiredService<UserManager<AppUser>>(); //Ask CLR Create Obj StoreIdentityDbContext
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync(); //Update-Database
                await StoreDbContextSeed.SeedAsync(context);
                await identityContext.Database.MigrateAsync();
                await StoreIdentityDbContextSeed.SeedAppUserAsync(userManager);
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();

                Logger.LogError(ex, message: "There are problem during applying migration !");
            }


            app.UseMiddleware<ExceptionMiddleWare>(); //Configure Users-Defined [ExceptionMiddleWare]   MiddleWare



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseStatusCodePagesWithReExecute(pathFormat: "/error/{0}");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            return app;

        }

    }
}
