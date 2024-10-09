using Microsoft.EntityFrameworkCore;
using Store.G04.Core.Sevices.Contract;
using Store.G04.Core;
using Store.G04.Repository;
using Store.G04.Repository.Data.Contexts;
using Store.G04.Service.Services.Products;
using System.Reflection.Metadata.Ecma335;
using Store.G04.Core.Mapping.Products;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Store.G04.APIs.Errors;

namespace Store.G04.APIs.Helper
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddDependancy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInService();
            services.AddBuiltInService();
            services.AddSwaggerService();
            services.AddDbContextService(configuration);
            services.AddUserDefinedService();
            services.AddAutoMapperService(configuration);
            services.ConfigureInvalidModelStatusResponseService();



            return services;
        }

        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {
            services.AddControllers();


            return services;
        }

        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            return services;
        }

        private static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(name: "DefaultConnection"));
            });

            return services;
        }

        private static IServiceCollection AddUserDefinedService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }

        private static IServiceCollection AddAutoMapperService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));


            return services;
        }

        private static IServiceCollection ConfigureInvalidModelStatusResponseService(this IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                             .SelectMany(P => P.Value.Errors)
                                             .Select(E => E.ErrorMessage)
                                             .ToArray();

                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });



            return services;
        }



    }
}
