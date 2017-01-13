using System.Linq;
using BookStore.Models;

namespace WebApiCoreDemo.Repository
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetAllOrders();
        IQueryable<Order> GetAllOrdersWithDetails(bool withdetail);
        Order GetOrder(int id);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void RemoveOrder(Order order);
    }
}