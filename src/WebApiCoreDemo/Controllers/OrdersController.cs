using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiCoreDemo.Repository;
using BookStore.Models;
using Microsoft.AspNetCore.Cors;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
=======
>>>>>>> 1b739fc3cc11d7b2293da68b370ba51f34cf0894

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCoreDemo.Controllers
{
    [Route("api/[Controller]")]
    [EnableCors("SiteCorsPolicy")]
    public class OrdersController : Controller
    {
        private IOrderRepository _repository;

        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id:int:min(1)}", Name = "GetOrder")]
        public IActionResult GetOrderById([Required]int id)
        {
            try
            {
                var order = _repository.GetOrder(id);
                if (order == null)
                    return NotFound();
                else
                    return Ok(order);
                    //return new ObjectResult(order);
            }
            catch(KeyNotFoundException)
            {
                Response.Headers.Add("x-status-reason", $"No resource was found with the unique identifier {id}");
                return NotFound();
            }
        }

        [HttpGet]
        public IQueryable<Order> GetAllOrders()
        {
            return _repository.GetAllOrders();
        }

        //[HttpGet]
        //public IQueryable<Order> GetAllOrdersWithDetails(bool withdetail)
        //{
        //    if(withdetail)
        //        return _repository.GetAllOrdersWithDetails(withdetail);

        //    return GetAllOrders();
        //}

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            _repository.AddOrder(order);
            return CreatedAtRoute("GetOrder", new { Controller = "Orders", id = "Customer" }, order);
        }
    }
}
