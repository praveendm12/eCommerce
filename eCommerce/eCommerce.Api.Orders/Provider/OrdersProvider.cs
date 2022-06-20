using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Api.Orders.Db;
using eCommerce.Api.Orders.Interfaces;
using eCommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCommerce.Api.Orders.Provider
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext _ordersDbContext;
        private readonly ILogger<OrdersProvider> _logger;
        private readonly IMapper _mapper;

        public OrdersProvider(OrdersDbContext ordersDbContext,
            ILogger<OrdersProvider> logger, IMapper mapper)
        {
            _ordersDbContext = ordersDbContext;
            _logger = logger;
            _mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if(_ordersDbContext.Orders.Any())
                return;
            ;
            _ordersDbContext.Orders.Add(new DbOrder()
            {
                CustomerId = 1,
                Id = 33,
                Items = new List<DbOrderItem>()
                {
                    new DbOrderItem() {Id = 2, OrderId = 33, ProductId = 2, Quantity = 1}
                }
            });

            _ordersDbContext.SaveChanges();
        }

        public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            var orders = await _ordersDbContext.Orders.Where(item => item.CustomerId == customerId).ToListAsync();

            if (orders != null && orders.Any())
            {
                var result = _mapper.Map<IEnumerable<DbOrder>, IEnumerable<Order>>(orders);
                return (true, result, "");
            }

            return (false, null, "Not Found");
        }
    }
}
