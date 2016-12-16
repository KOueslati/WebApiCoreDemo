using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiCoreDemo.Repository;
using BookStore.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCoreDemo.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private IOrderRepository _repository;

        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult GetOrderById(int id)
        {
            var order = _repository.GetOrder(id);
            if (order == null)
                return NotFound();
            else
                return new ObjectResult(order);
        }

        [HttpGet]
        public IQueryable<Order> GetAllOrders()
        {
            return _repository.GetAllOrders();
        }

        [HttpGet]
        public IQueryable<Order> GetAllOrdersWithDetails(bool withdetail)
        {
            if(withdetail)
                return _repository.GetAllOrdersWithDetails(withdetail);

            return GetAllOrders();
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            _repository.AddOrder(order);
            return CreatedAtRoute("GetOrder", new { Controller = "Order", id = "Customer" }, order);
        }
    }
}
