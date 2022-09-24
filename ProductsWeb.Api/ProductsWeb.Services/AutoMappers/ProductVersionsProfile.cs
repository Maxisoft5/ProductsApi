using AutoMapper;
using Products.DataAccessEfCore;
using Products.DTO;

namespace ProductsWeb.Services.AutoMappers
{
    public class ProductVersionsProfile : Profile
    {
        public ProductVersionsProfile()
        {
            CreateMap<ProductVersion, ProductVersionDTO>().ReverseMap();
        }
    }
}
