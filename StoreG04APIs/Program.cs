
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Store.G04.APIs.Errors;
using Store.G04.APIs.Helper;
using Store.G04.APIs.MiddleWares;
using Store.G04.Core;
using Store.G04.Core.Mapping.Products;
using Store.G04.Core.Sevices.Contract;
using Store.G04.Repository;
using Store.G04.Repository.Data;
using Store.G04.Repository.Data.Contexts;
using Store.G04.Service.Services.Products;
 

namespace StoreG04APIs
{
    public class Program
    {

        // Entry Point
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.

            builder.Services.AddDependancy(builder.Configuration);

            var app = builder.Build();

            
            await app.ConfigureMiddleWaresAsync();

            app.Run();
        }
    }
}
