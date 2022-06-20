using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Api.Orders.Db
{
    public class OrdersDbContext : DbContext
    {
        public DbSet<DbOrder> Orders { get; set; }


        public OrdersDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
