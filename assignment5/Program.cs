using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework5
{
    public class Customer
    {
        public string Name { get; set; }
        public string ID { get; }
        public Customer(string name, string id) { Name = name; ID = id; }

        public override string ToString() { return $"Name:{Name} ID:{ID}"; }
    }

    public class Product
    {
        public string Name { get; }
        public string Id { get; }
        public int Price { get; }
        public Product(string name, string id, int price) { Name = name; Id = id; Price = price; }

        public override string ToString()
        {
            return $"Name:{Name} ID:{Id}  Price:{Price}";
        }
    }
    public class Order
    {
        public string ID { get; }
        public Customer Customer { get; }
        public List<OrderDetails> Details { get; }

        public int TotalSum { get; private set; }

        private void UpdateTotalSum()
        {
            TotalSum = Details.Sum(d => d.Total);
        }

        public Order(string id, Customer customer, List<OrderDetails> details)
        {
            ID = id;
            Customer = customer;
            Details = details;
            UpdateTotalSum();

        }
        public override string ToString()
        {
            string details = "";
            Details.ForEach(item =>
            {
                details = details + "    " + item.ToString() + "\n";
            });
            return $"OrderID:{ID}\n" +
                $"CustomerInfo  {Customer} \n" +
                $"TotalSum {TotalSum}\n" +
                $"Details\n" +
                details
                ;
        }
        public override bool Equals(object obj)
        {
            return obj is Order order &&
            this.ID == order.ID;
        }

    }
    public class OrderDetails
    {
        public Product Product { get; }
        public int Quantities { get; }
        public int Total => Product.Price * Quantities;
        public override string ToString()
        {
            return $"{Product} Quantities:{Quantities} Total:{Total}";
        }
        public override bool Equals(object obj)
        {
            return obj is OrderDetails details &&
                   Product.Id == details.Product.Id &&
                   Quantities == details.Quantities;
        }
    }
    public class OrderService
    {
        List<Order> orders = new List<Order>();
        public void AddOrder(string ID, Customer Customer, List<OrderDetails> Details)
        {
            Order newOrder = new Order(ID, Customer, Details);
            orders.ForEach(item =>
            {
                if (item == newOrder) { throw new InvalidOperationException(" Repeteated Order!"); }
            });
            orders.Add(newOrder);
        }
        public void DeleteOrder(string ID)
        {
            int deleted = orders.RemoveAll(item => item.ID == ID);
            if (deleted == 0) { throw new InvalidOperationException("NO Such File!"); }
        }
        public void ChangeOrder(string ID, Customer Customer, List<OrderDetails> Details)
        {
            bool found = false;
            orders.ForEach(item =>
            {
                if (item.ID == ID)
                {
                    found = true;
                    item = new Order(ID, Customer, Details);
                }
            });
            if (!found) { throw new InvalidOperationException("NO Such Order!"); }
        }

        public List<Order> Search(Func<Order, bool> search)
        {
            var searchlist = orders.Where(search)
                    .OrderBy(o => o.TotalSum)
                    .ToList();
            if (!searchlist.Any())
            {
                throw new InvalidOperationException("No Such Order!");
            };
            return searchlist;
        }
        public void Sort() => Sort((o1, o2) =>
            string.Compare(o1.ID, o2.ID, StringComparison.Ordinal) > 0);
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
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

}
