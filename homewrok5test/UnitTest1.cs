using homework5;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework5.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_ShouldInitializeProperties()
        {
            var customer = new Customer("John", "C001");
            Assert.Equal("John", customer.Name);
            Assert.Equal("C001", customer.ID);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            var customer = new Customer("Alice", "C002");
            Assert.Equal("Name:Alice ID:C002", customer.ToString());
        }
    }

    public class ProductTests
    {
        [Fact]
        public void Product_ShouldInitializeProperties()
        {
            var product = new Product("Laptop", "P001", 1500);
            Assert.Equal("Laptop", product.Name);
            Assert.Equal("P001", product.Id);
            Assert.Equal(1500, product.Price);
        }

        [Fact]
        public void ToString_ShouldContainProductInfo()
        {
            var product = new Product("Mouse", "P002", 50);
            Assert.Contains("Name:Mouse", product.ToString());
            Assert.Contains("ID:P002", product.ToString());
            Assert.Contains("Price:50", product.ToString());
        }
    }

    public class OrderDetailsTests
    {
        [Fact]
        public void Total_ShouldCalculateCorrectly()
        {
            var product = new Product("Keyboard", "P003", 80);
            var detail = new OrderDetails(product, 3);
            Assert.Equal(240, detail.Total);
        }

        [Fact]
        public void Equals_ShouldCompareByProductAndQuantity()
        {
            var product = new Product("HDMI Cable", "P004", 15);
            var detail1 = new OrderDetails(product, 2);
            var detail2 = new OrderDetails(product, 2);
            var detail3 = new OrderDetails(product, 3);

            Assert.True(detail1.Equals(detail2));
            Assert.False(detail1.Equals(detail3));
        }
    }

    public class OrderTests
    {
        private Order CreateTestOrder() => new(
            "O001",
            new Customer("Bob", "C003"),
            new List<OrderDetails>
            {
                new(new Product("Monitor", "P005", 300), 2),
                new(new Product("Dock", "P006", 120), 1)
            });

        [Fact]
        public void TotalSum_ShouldCalculateCorrectly()
        {
            var order = CreateTestOrder();
            Assert.Equal(720, order.TotalSum);
        }

        [Fact]
        public void Equals_ShouldCompareByOrderId()
        {
            var order1 = CreateTestOrder();
            var order2 = new Order("O001", new("Different", "C004"), new List<OrderDetails>());
            var order3 = new Order("O002", new("Bob", "C003"), new List<OrderDetails>());

            Assert.True(order1.Equals(order2));
            Assert.False(order1.Equals(order3));
        }
    }

    public class OrderServiceTests
    {
        private OrderService CreateTestService()
        {
            var service = new OrderService();
            var customer = new Customer("Test", "TC001");
            var products = new[]
            {
                new Product("Test1", "TP1", 100),
                new Product("Test2", "TP2", 200)
            };

            service.AddOrder("T001", customer, new List<OrderDetails>
            {
                new(products[0], 2),
                new(products[1], 1)
            });

            service.AddOrder("T002", customer, new List<OrderDetails>
            {
                new(products[1], 3)
            });

            return service;
        }

        [Fact]
        public void AddOrder_ShouldRejectDuplicateIds()
        {
            var service = CreateTestService();
            var ex = Assert.Throws<InvalidOperationException>(() =>
                service.AddOrder("T001", new("New", "NC1"), new List<OrderDetails>()));
            Assert.Contains("Repeteated", ex.Message);
        }

        [Fact]
        public void DeleteOrder_ShouldRemoveCorrectOrder()
        {
            var service = CreateTestService();
            service.DeleteOrder("T001");
            Assert.Single(service.GetOrders());
        }

        [Fact]
        public void ChangeOrder_ShouldUpdateCorrectOrder()
        {
            var service = CreateTestService();
            var newCustomer = new Customer("Updated", "UC1");
            var newDetails = new List<OrderDetails>
            {
                new(new Product("NewProd", "NP1", 500), 1)
            };

            service.ChangeOrder("T001", newCustomer, newDetails);
            var changedOrder = service.GetOrders().First(o => o.ID == "T001");

            Assert.Equal(newCustomer.ID, changedOrder.Customer.ID);
            Assert.Equal(500, changedOrder.TotalSum);
        }

        [Fact]
        public void Search_ShouldReturnFilteredResults()
        {
            var service = CreateTestService();
            var results = service.Search(o => o.TotalSum > 400);
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public void Sort_ShouldOrderByIdDescending()
        {
            var service = CreateTestService();
            service.AddOrder("A001", new("Test", "TC1"), new List<OrderDetails>());
            service.Sort();

            var orders = service.GetOrders().ToList();
            Assert.Equal("A001", orders[0].ID);
            Assert.Equal("T002", orders[1].ID);
            Assert.Equal("T001", orders[2].ID);
        }
    }

   
}