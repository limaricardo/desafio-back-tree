using DesafioBackTree.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackTree.Data
{
    public class Context: IdentityDbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) 
        { 
       
        }
        public DbSet<Product> Products { get; set; }
    }
}
