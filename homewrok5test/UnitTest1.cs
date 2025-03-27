using homework5;
using Microsoft.VisualStudio.TestTools.UnitTesting; // 使用原生 MSTest
using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework5.Tests
{
    [TestClass] 
    public class CustomerTests
    {
        [TestMethod] 
        public void Customer_ShouldInitializeProperties()
        {
            var customer = new Customer("John", "C001");
            Assert.AreEqual("John", customer.Name); 
            Assert.AreEqual("C001", customer.ID);
        }

        [TestMethod]
        public void ToString_ShouldReturnFormattedString()
        {
            var customer = new Customer("Alice", "C002");
            Assert.AreEqual("Name:Alice ID:C002", customer.ToString());
        }
    }

    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void Product_ShouldInitializeProperties()
        {
            var product = new Product("Laptop", "P001", 1500);
            Assert.AreEqual("Laptop", product.Name);
            Assert.AreEqual("P001", product.Id);
            Assert.AreEqual(1500, product.Price);
        }

        [TestMethod]
        public void ToString_ShouldContainProductInfo()
        {
            var product = new Product("Mouse", "P002", 50);
            StringAssert.Contains(product.ToString(), "Name:Mouse");
            StringAssert.Contains(product.ToString(), "ID:P002");
            StringAssert.Contains(product.ToString(), "Price:50");
        }
    }

 

    [TestClass]
    public class OrderServiceTests
    {
     

        [TestMethod]
        public void AddOrder_ShouldRejectDuplicateIds()
        {
            var service = new OrderService();
            var ex = Assert.ThrowsException<InvalidOperationException>(() =>
                service.AddOrder("T001", new Customer("New", "NC1"), new List<OrderDetails>()));
            StringAssert.Contains(ex.Message, "Repeteated");
        }

        [TestMethod]
        public void Search_ShouldReturnFilteredResults()
        {
            var service = new OrderService();
            var results = service.Search(o => o.TotalSum > 400);
            Assert.IsTrue(results.Count() == 2); 
        }
    }
}