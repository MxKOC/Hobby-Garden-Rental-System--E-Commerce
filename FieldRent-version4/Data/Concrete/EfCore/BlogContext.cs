using FieldRent.Entity;
using Microsoft.EntityFrameworkCore;

namespace FieldRent.Data.Concrete.EfCore
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }
        public DbSet<Map> Maps => Set<Map>();
        public DbSet<Field> Fields => Set<Field>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Request> Requests => Set<Request>();
        public DbSet<PurchaseHistory> PurchaseHistories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    }
}