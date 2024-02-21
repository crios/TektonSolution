using Tekton.Products.Entity.Interfaces;
namespace Tekton.Products.RepositoryLayer.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<IProduct>> GetAllProducts();
        Task<IProduct> GetProductById(int productId);
        Task AddProduct(IProduct product);
        Task UpdateProduct(IProduct product);
    }

}
