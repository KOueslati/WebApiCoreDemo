using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApiCoreDemo.Controllers;
using WebApiCoreDemo.Models;
using WebApiCoreDemo.Repository;
using Xunit;

namespace XUnitTestWebApiCore
{
    public class OrdersControllerTest
    {
        private OrderRepository _repository;

        [Fact]
        public void CanCreateOrder()
        {
            _repository = new OrderRepository(CreateAndSeedContext());
            using (var controller = new OrdersController(_repository))
            {
                var order = new Order()
                {
                    Customer = "khaled oueslati",
                    OrderDate = new DateTime(2015, 12, 30)
                };

                var result = controller.CreateOrder(order);
                if (result is ObjectResult)
                {
                    var orderResult = ((ObjectResult)result).Value;
                    Assert.NotNull((orderResult as Order).Id);
                    Assert.Equal((orderResult as Order).Customer, "khaled oueslati");
                }
            }
        }

        // Init DBcontext on memory

        private WebApiCoreContext CreateAndSeedContext()
        {
            var optionBuilder = new DbContextOptionsBuilder<WebApiCoreContext>();
            optionBuilder.UseInMemoryDatabase();

            var context = new WebApiCoreContext(optionBuilder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

    }
}
