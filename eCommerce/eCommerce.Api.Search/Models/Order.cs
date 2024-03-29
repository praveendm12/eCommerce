﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public List<OrderItem> Items { get; set; }

        public decimal Total { get; set; }
    }
}
