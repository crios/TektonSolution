using Tekton.Products.Entity.Interfaces;
namespace Tekton.Products.Entity.Implementations
{
    public class Product : IProduct
    {
        // Propiedades de la clase Product
        public int productId { get; set; }
        public string name { get; set; }
        public int status { get; set; }
        public bool stock { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
    }

}
