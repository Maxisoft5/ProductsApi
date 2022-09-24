using ProductsWeb.Services.AutoMappers;

namespace ProductsWeb.Api.Setup
{
    public class AutoMapperConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(ProductVersionsProfile));
        }
    }
}
