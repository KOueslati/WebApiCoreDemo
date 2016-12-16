using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCoreDemo.Models
{
    public class WebApiCoreContext : DbContext
    {
        public WebApiCoreContext(DbContextOptions<WebApiCoreContext> options):
            base(options)
        { }

        public WebApiCoreContext()
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}

