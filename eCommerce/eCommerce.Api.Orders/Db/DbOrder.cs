using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Orders.Db
{
    public class DbOrder
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public List<DbOrderItem> Items { get; set; }

        public decimal Total { get; set; }
    }

    public class DbOrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

    }
}
