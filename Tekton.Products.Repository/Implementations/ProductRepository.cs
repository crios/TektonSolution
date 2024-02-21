using Microsoft.EntityFrameworkCore;
using Tekton.Products.Entity.Implementations;
using Tekton.Products.Entity.Interfaces;
using Tekton.Products.RepositoryLayer.Context;
using Tekton.Products.RepositoryLayer.Interfaces;
namespace Tekton.Products.RepositoryLayer.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IProduct>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IProduct> GetProductById(int productId)
        {
            return await _context.Products
            .Where(p => p.productId == productId)
            .FirstOrDefaultAsync();
        }

        public async Task AddProduct(IProduct product)
        {
            var concreteProduct = new Product
            {
                name = product.name,
                status = product.status,
                stock = product.stock,
                description = product.description,
                price = product.price
            };

            _context.Products.Add(concreteProduct);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateProduct(IProduct product)
        {
            var existingProduct = await _context.Products.FindAsync(product.productId);
            if (existingProduct != null)
            {
                existingProduct.name = product.name;
                existingProduct.status = product.status;
                existingProduct.stock = product.stock;
                existingProduct.description = product.description;
                existingProduct.price = product.price;
                await _context.SaveChangesAsync();
            }
        }
    }

}
