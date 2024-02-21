using Tekton.Products.Entity.Interfaces;
namespace Tekton.Products.BusinessLayer.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<IProduct>> GetAllProducts();
        Task<IProduct> GetProductById(int productId);
        Task AddProduct(IProduct product);
        Task UpdateProduct(int productId, IProduct product);
    }
}

