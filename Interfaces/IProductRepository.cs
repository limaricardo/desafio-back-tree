using DesafioBackTree.Models;

namespace DesafioBackTree.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();

        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        Product GetProduct(int id);
        bool ProductExists(int id);
        bool Save();
    }
}
