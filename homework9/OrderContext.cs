using Microsoft.EntityFrameworkCore;
using homework5;
namespace homework9
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
         : base(options)
        {
            this.Database.EnsureCreated(); //自动建库建表
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<OrderDetails> OrderDetails { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
    }
}