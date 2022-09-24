using AutoMapper;
using Microsoft.Extensions.Logging;
using Products.DataAccessEfCore;
using Products.DataAccessEfCore.Repositories.Interfaces;
using Products.DTO;
using ProductsWeb.Services.Interfaces;

namespace ProductsWeb.Services.Services
{
    public class ProductsService : IProductsService, IBaseService<ProductDTO>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsService> _logger;
        public ProductsService(IProductRepository productRepository, IMapper mapper, ILogger<ProductsService> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductDTO> Create(ProductDTO entity)
        {
            var product = _mapper.Map<Product>(entity);
            product.Id = Guid.NewGuid();
            if (await _productRepository.IsExists(_mapper.Map<Product>(entity)))
            {
                _logger.LogWarning($"Product with name ${entity.Name} has already existed");
                throw new ArgumentException($"Product with name ${entity.Name} has already existed");
            }

            var result = await _productRepository.Create(product);
            return _mapper.Map<ProductDTO>(result);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _productRepository.Delete(id);
        }

        public async Task<ProductDTO> Edit(ProductDTO entity)
        {
            var product = _mapper.Map<Product>(entity);
            if (await _productRepository.IsExists(_mapper.Map<Product>(entity), true))
            {
                _logger.LogWarning($"Product with name ${entity.Name} has already existed");
                throw new ArgumentException($"Product with name ${entity.Name} has already existed");
            }
            var result = await _productRepository.Edit(product);
            return _mapper.Map<ProductDTO>(result);
        }

        public async Task<IEnumerable<FindProductDTOResult>> Find(string productName, string productVersionName, 
            decimal minSize, decimal maxSize)
        {
            var result = await _productRepository.Find(productName, productVersionName, minSize, maxSize);
            return result;
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var result = await _productRepository.GetAll();
            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task<ProductDTO> GetById(Guid id)
        {
            var result = await _productRepository.GetById(id);
            return _mapper.Map<ProductDTO>(result);
        }

        public async Task<ProductDTO> GetByName(string productName)
        {
            var result = await _productRepository.GetByName(productName);
            return _mapper.Map<ProductDTO>(result);
        }

        public async Task<bool> IsExists(ProductDTO entity)
        {
            return await _productRepository.IsExists(_mapper.Map<Product>(entity));
        }
    }
}
