using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Orders.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public List<OrderItem> Items { get; set; }

        public decimal Total { get; set; }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }

        public int Quantity { get; set; }

    }
}
