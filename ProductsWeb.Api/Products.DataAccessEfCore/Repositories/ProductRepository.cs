using Microsoft.EntityFrameworkCore;
using Products.DataAccessEfCore.Repositories.Interfaces;
using Products.DTO;

namespace Products.DataAccessEfCore.Repositories
{
    public class ProductRepository : IBaseRepository<Product>, IProductRepository
    {
        private readonly TestDbContext _testDbContext;
        public ProductRepository(TestDbContext testDbContext)
        {
            _testDbContext = testDbContext;
        }

        public async Task<Product> Create(Product entity)
        {
            await _testDbContext.Products.AddAsync(entity);
            await _testDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            var product = await _testDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return false;
            }
            _testDbContext.Products.Remove(product);
            await _testDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Product> Edit(Product entity)
        {
            var existEntity = await _testDbContext.Products
                .Include(x => x.ProductVersions).FirstOrDefaultAsync(x => x.Id == entity.Id);

            existEntity.Name = entity.Name;
            existEntity.Description = entity.Description;

            foreach (var pv in entity.ProductVersions)
            {
                var existsProductVersion = existEntity.ProductVersions.FirstOrDefault(x => x.Id == pv.Id);
                if (existsProductVersion == null)
                {
                    pv.Id = Guid.NewGuid();
                    pv.ProductId = entity.Id;
                    existEntity.ProductVersions.Add(pv);
                }
                else
                {
                    existsProductVersion.Name = pv.Name;
                    existsProductVersion.Description = pv.Description;
                    existsProductVersion.CreatingDate = pv.CreatingDate;
                }
            }
            
            _testDbContext.Products.Update(existEntity);
            await _testDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<FindProductDTOResult>> Find(string productName, string productVersionName, decimal minSize, decimal maxSize)
        {
            var results = _testDbContext.Set<FindProductDTOResult>()
                .FromSqlRaw($"exec ProductSearch '{productName}', '{productVersionName}', {minSize}, {maxSize}");

            return await results.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _testDbContext.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _testDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> GetByName(string productName)
        {
            return await _testDbContext.Products.Include(x => x.ProductVersions)
                                        .FirstOrDefaultAsync(x => x.Name.ToLower() == productName.ToLower());
        }

        public async Task<bool> IsExists(Product entity, bool forUpdate = false)
        {
            if (forUpdate)
            {
                return await _testDbContext.Products.AnyAsync(x => x.Name == entity.Name && x.Id != entity.Id);
            } 
            return await _testDbContext.Products.AnyAsync(x => x.Name == entity.Name || x.Id == entity.Id);
        }
    }
}
