using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingCart_infrastructure.Context;
using ShoppingCart_infrastructure.Repositories;

namespace ShoppingCart_infrastructure
{
    public static class ServiceConfigure
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

            services.AddDbContext<ShoppingCartContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            return services;
        }
    }
}
