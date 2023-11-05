using DesafioBackTree.Data;
using DesafioBackTree.Models;

namespace DesafioBackTree
{
    public class Seed
    {
        private readonly Context context;
        public Seed(Context context)
        {
            this.context = context;
        }
        public void SeedContext()
        {
            if (!context.Products.Any())
            {
                var products = new List<Product>()
                {
                    new Product()
                    {
                        Name = "Banana",
                        Description = "Fruit",
                        Price = 2.50m
                    },
                };
                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
