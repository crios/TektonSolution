using Tekton.Products.BusinessLayer.Interfaces;
using Tekton.Products.Entity.Interfaces;
using Tekton.Products.RepositoryLayer.Interfaces;

namespace Tekton.Products.BusinessLayer.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        /// <summary>
        /// Get All Products
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<IProduct>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();
            return products;
        }

        /// <summary>
        /// Get a specific product by Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<IProduct> GetProductById(int productId)
        {
            return await _productRepository.GetProductById(productId);
        }

        /// <summary>
        /// Save a new product 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task AddProduct(IProduct product)
        {
            await _productRepository.AddProduct(product);
        }


        /// <summary>
        ///  Update an existing product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task UpdateProduct(int productId, IProduct product)
        {
            // Verificar si el producto existe antes de actualizarlo
            var existingProduct = await _productRepository.GetProductById(productId);
            if (existingProduct != null)
            {
                // Actualizar solo las propiedades necesarias
                existingProduct.name = product.name;
                existingProduct.status = product.status;
                existingProduct.stock = product.stock;
                existingProduct.description = product.description;
                existingProduct.price = product.price;
                await _productRepository.UpdateProduct(existingProduct);
            }
        }
    }

}
