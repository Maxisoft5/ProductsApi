using AutoMapper;
using Products.DataAccessEfCore;
using Products.DTO;

namespace ProductsWeb.Services.AutoMappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
