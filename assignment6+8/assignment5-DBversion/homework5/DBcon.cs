using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework5
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class OrderringContext : DbContext
    {
        public OrderringContext() : base("OrderDataBase")
        {
            Database.SetInitializer(
            new DropCreateDatabaseIfModelChanges<OrderringContext>());
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
