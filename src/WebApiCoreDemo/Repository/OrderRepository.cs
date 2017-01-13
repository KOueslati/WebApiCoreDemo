using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoreDemo.Models;

namespace WebApiCoreDemo.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private WebApiCoreContext _dbcontext;

        public OrderRepository(WebApiCoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Order GetOrder(int id)
        {
            return _dbcontext.Orders.Include(o => o.OrderDetails).ThenInclude(d => d.Book).FirstOrDefault(o => o.Id == id);
        }

        public IQueryable<Order> GetAllOrders()
        {
            return _dbcontext.Orders;
        }

        public IQueryable<Order> GetAllOrdersWithDetails(bool withdetail)
        {
            return _dbcontext.Orders.Include( o => o.OrderDetails);
        }

        public void AddOrder(Order order)
        {
            _dbcontext.Orders.Add(order);
        }
    }
}
