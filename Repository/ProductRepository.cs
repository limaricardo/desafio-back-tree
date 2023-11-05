using DesafioBackTree.Data;
using DesafioBackTree.Interfaces;
using DesafioBackTree.Models;

namespace DesafioBackTree.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;
        public ProductRepository(Context context) 
        { 
            _context = context;
        }

        public bool CreateProduct(Product product)
        {
            _context.Add(product);

            return Save();
        }

        public ICollection<Product> GetProducts() 
        {
            return _context.Products.OrderBy(p => p.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }
    }
}
