using Products.DataAccessEfCore;
using Products.DTO;

namespace ProductsWeb.Services.Interfaces
{
    public interface IProductsService : IBaseService<ProductDTO>
    {
        public Task<IEnumerable<FindProductDTOResult>> Find(string productName, string productVersionName, decimal minSize, decimal maxSize);
        public Task<ProductDTO> GetByName(string productName);
    }
}
