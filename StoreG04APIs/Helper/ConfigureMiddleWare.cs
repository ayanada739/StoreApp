using Store.G04.APIs.MiddleWares;
using Store.G04.Repository.Data.Contexts;
using Store.G04.Repository.Data;
using StoreG04APIs;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace Store.G04.APIs.Helper
{
    public static class ConfigureMiddleWare 
    {
        public static async Task<WebApplication> ConfigureMiddleWaresAsync(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<StoreDbContext>(); //Ask CLR Create Obj StoredDbContext
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync(); //Update-Database
                await StoreDbContextSeed.SeedAsync(context);

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

            app.UseAuthorization();


            app.MapControllers();

            return app;

        }

    }
}
