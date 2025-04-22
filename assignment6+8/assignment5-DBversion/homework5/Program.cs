using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;          
using System.ComponentModel.DataAnnotations.Schema;
using MySql.Data.MySqlClient;
using MySql.Data.EntityFramework;
using System.Data.Entity;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.X509;

namespace homework5
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public Customer() { }
        public Customer(string name) { Name = name; }

        public override string ToString() { return $"Name:{Name} ID:{CustomerID}"; }
    }

    public class Product
    {
        [Key]
        public int ProductID { get; set; }


        public string Name { get; set; }
       
        public double Price { get; set; }
        public Product() { }
        public Product(string name,  double price) { Name = name;  Price = price; }

        public override string ToString()
        {
            return $"Name:{Name} ID:{ProductID}  Price:{Price}";
        }
    }
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [Required]
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }
        public List<OrderDetails> Details { get; set; }

        public double TotalSum => Details?.Sum(d => d.Total) ?? 0;

        public Order() { }
        public Order( Customer customer, List<OrderDetails> details)
        {
            Customer = customer;
            Details = details;
        }
        public override string ToString()
        {
            string details = "";
            Details.ForEach(item =>
            {
                details = details + "    " + item.ToString() + "\n";
            });
            return $"OrderID:{OrderID}\n" +
                $"CustomerInfo  {Customer} \n" +
                $"TotalSum {TotalSum}\n" +
                $"Details\n" +
                details
                ;
        }
        public override bool Equals(object obj)
        {
            return obj is Order order &&
            this.OrderID == order.OrderID;
        }

    }
    public class OrderDetails
    {
        [Key]
        public int DetailID { get; set; }
      
        
        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public Order Order { get; set; }

        
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public Product Product { get; set; }
        public int Quantities { get; set; }
        public double Total => Product.Price * Quantities;
        public override string ToString()
        {
            return $"{Product} Quantities:{Quantities} Total:{Total}";
        }
        public OrderDetails() { }
        public OrderDetails(Product product, int quantities)
        {
            Product = product;
            Quantities = quantities;

        }
        public String ProductName { get => Product != null ? this.Product.Name : ""; }
        public override bool Equals(object obj)
        {
            var item = obj as OrderDetails;
            return item != null &&
                   ProductName == item.ProductName;
        }
    }
    public class OrderService
    {
        public OrderService() {
            using (var ctx = new OrderringContext())
            {
                Product p1 = new Product("apple", 100.0) { ProductID = 1 };
                Product p2 = new Product("egg", 200.0) { ProductID = 2 };   
                Customer c1 = new Customer("li") { CustomerID = 1 };
                Customer c2 = new Customer("zhang") { CustomerID= 2 };
                if (ctx.Products.Count() <= 1)
                {
                    ctx.Products.Add(p1);
                    ctx.Products.Add(p2);
                    ctx.SaveChanges();
                }
                if (ctx.Customers.Count() <= 1)
                {
                    ctx.Customers.Add(c1);
                    ctx.Customers.Add(c2);
                    ctx.SaveChanges();
                }
                if (ctx.Orders.Count() == 0) { 
                   List<OrderDetails> list= new List<OrderDetails> { new OrderDetails(p1,1) };
                   AddOrder(new Order(c1, list) { OrderID=1});
                }
            }
        }
        public List<Order> orders {
        get{
            using(var con=new OrderringContext())
                {
                    return con.Orders
                        .Include (o => o.Details.Select(d=>d.Product))
                        .Include(o=>o.Customer)
                        .ToList<Order>();
                }
            }
        }
        public void AddOrder(Order newOrder)
        {
            
            FixOrder(newOrder);
            using(var ctx = new OrderringContext()) {
                ctx.Orders.Add(newOrder);
                ctx.SaveChanges();
            }
        }
        public void DeleteOrder(int OrderID)
        {
            using (var ctx = new OrderringContext())
            {
                var order = ctx.Orders.Include("Details")
                  .SingleOrDefault(o => o.OrderID == OrderID);
                if (order == null) return;
                ctx.OrderDetails.RemoveRange(order.Details);
                ctx.Orders.Remove(order);
                ctx.SaveChanges();
            }
        }
        public void ChangeOrder(Order newOrder)
        {
            DeleteOrder(newOrder.OrderID);
            AddOrder(newOrder);
        }

        public List<Order> Search(Func<Order, bool> search)
        {
            using (var ctx = new OrderringContext())
            {
                return ctx.Orders
                  .Include(o => o.Details.Select(d => d.Product))
                  .Include("Customer")
                .Where(search)
                .ToList();
            }
        }
        public void Sort() => Sort((o1, o2) =>
            o1.OrderID>o2.OrderID);
        public void Sort(Func<Order, Order, bool> Compare)
        {
            int lenth = orders.Count();
            for (int i = 0; i < lenth - 1; i++)
            {
                for (int j = 0; j < lenth - 1 - i; j++)
                {
                    if (Compare(orders[i], orders[j]))
                    {
                        Order temp = orders[i];
                        orders[i] = orders[j];
                        orders[j] = temp;
                    }

                }
            }
        }
        private static void FixOrder(Order newOrder)
        {
            newOrder.CustomerID = newOrder.Customer.CustomerID;
            newOrder.Customer = null;
            newOrder.Details.ForEach(d => {
                d.ProductID = d.Product.ProductID;
                d.Product = null;
            });
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

}
