using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using homework9;
using homework5;

namespace homework9.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly OrderContext orderDb;

        public OrderController(OrderContext context)
        {
            this.orderDb = context;
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            Console.WriteLine("");
            var order = orderDb.Orders.FirstOrDefault(t => t.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpGet]
        public ActionResult<List<Order>> GetOrders(string? Customername)
        {
            var query = buildQuery(Customername);
            return query.ToList();
        }


        private IQueryable<Order> buildQuery(string? name)
        {
            IQueryable<Order> query = orderDb.Orders;
            if (name != null)
            {
                query = query.Where(t => t.Customer.Name.Contains(name));
            }
            return query;
        }


        [HttpPost]
        public ActionResult<Order> PostOrder(Order order)
        {
            try
            {
                orderDb.Orders.Add(order);
                orderDb.SaveChanges();
            }
            catch (Exception e)
            {
                String error = (e.InnerException == null) ? e.Message
                    : e.InnerException.Message;
                return BadRequest(error);
            }
            return order;
        }
        [HttpPut("{id}")]
        public ActionResult<Order> PutOrder(int id, Order order)
        {
            if (id != order.OrderID)
            {
                return BadRequest("Id cannot be modified!");
            }
            try
            {
                orderDb.Entry(order).State = EntityState.Modified;
                orderDb.SaveChanges();
            }
            catch (Exception e)
            {
                String error = (e.InnerException == null) ? e.Message
                    : e.InnerException.Message;
                return BadRequest(error);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            try
            {
                var order = orderDb.Orders.FirstOrDefault(t => t.OrderID == id);
                if (order != null)
                {
                    orderDb.Remove(order);
                    orderDb.SaveChanges();
                }
            }
            catch (Exception e)
            {
                String error = (e.InnerException == null) ? e.Message
                   : e.InnerException.Message;
                return BadRequest(error);
            }
            return NoContent();
        }

    }
}