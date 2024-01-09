using Microsoft.Extensions.DependencyInjection;
using ShoppingCart_Application.Services.Commands.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart_Application
{
    public static class DI
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddProductCommand).Assembly));

            return services;
        }
    }
}
