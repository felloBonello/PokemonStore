using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace PokemonStore.Models
{

    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Brand>().ForSqlServerToTable("Brands")
                .Property(b => b.Id).UseSqlServerIdentityColumn();
            builder.Entity<Product>().ForSqlServerToTable("Products");

            builder.Entity<ShoppingCart>().ForSqlServerToTable("ShoppingCarts")
                .Property(t => t.Id).UseSqlServerIdentityColumn();
            builder.Entity<CartItem>().ForSqlServerToTable("CartItems")
                .Property(ti => ti.Id).UseSqlServerIdentityColumn();
            builder.Entity<Branch>().ForSqlServerToTable("Branches")
                .Property(s => s.Id).UseSqlServerIdentityColumn();
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
    }
}
