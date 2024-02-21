namespace Tekton.Products.Entity.Interfaces
{
    public interface IProduct
    {
        int productId { get; set; }
        string name { get; set; }
        int status { get; set; }
        bool stock { get; set; }
        string description { get; set; }
        decimal price { get; set; }
    }

}
