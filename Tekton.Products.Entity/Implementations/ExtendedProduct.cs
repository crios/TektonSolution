using Tekton.Products.Entity.Interfaces;
namespace Tekton.Products.Entity.Implementations
{
    public class ExtendedProduct : IProduct
    {
        #region IProduct
        public int productId { get; set; }
        public string name { get; set; }
        public int status { get; set; }
        public bool stock { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        #endregion

        #region Additional properties
        public string statusName { get; set; }
        public decimal discount { get; set; }
        public decimal finalPrice { get; set; }
        #endregion

    }
}
