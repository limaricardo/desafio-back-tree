using DesafioBackTree.Models;

namespace DesafioBackTree.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();

        bool CreateProduct(Product product);
        bool Save();
    }
}
