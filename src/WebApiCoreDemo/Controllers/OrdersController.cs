using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiCoreDemo.Repository;
using BookStore.Models;
using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;



// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCoreDemo.Controllers
{
    [Route("api/[Controller]")]
    [EnableCors("SiteCorsPolicy")]
    [Authorize]
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

        [AllowAnonymous]
        [HttpGet]
        public IQueryable<Order> GetAllOrders()
        {
            return _repository.GetAllOrders();
        }

        [HttpGet("{withdetail}")]
        public IQueryable<Order> GetAllOrdersWithDetails(bool withdetail)
        {
            if (withdetail)
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
            return CreatedAtRoute("GetOrder", new { Controller = "Orders", id = order.Id }, order);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOrder(int id, [FromBody]Order order)
        {
            var command = _repository.GetOrder(id);
            if (command == null)
                return NotFound(order);
            else
            {
                _repository.UpdateOrder(order);
                return NoContent();
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOrder(int id)
        {
            var command = _repository.GetOrder(id);
            if (command == null)
                return NotFound();
            else
            {
                _repository.RemoveOrder(command);
                return Ok();
            }
        }
    }
}
