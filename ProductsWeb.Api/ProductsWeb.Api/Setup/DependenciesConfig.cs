using Microsoft.EntityFrameworkCore;
using Products.DataAccessEfCore;
using Products.DataAccessEfCore.Repositories;
using Products.DataAccessEfCore.Repositories.Interfaces;
using ProductsWeb.Services.Interfaces;
using ProductsWeb.Services.Services;

namespace ProductsWeb.Api.Setup
{
    public class DependenciesConfig
    {
        public static void ConfigureDependencies(IServiceCollection services, string connectionString)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddDbContext<TestDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
