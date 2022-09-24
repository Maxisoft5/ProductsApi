using Products.DTO;

namespace Products.DataAccessEfCore.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        public Task<IEnumerable<FindProductDTOResult>> Find(string productName, string productVersionName, decimal minSize, decimal maxSize);
        public Task<Product> GetByName(string productName);
    }
}
