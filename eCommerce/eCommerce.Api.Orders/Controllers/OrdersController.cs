using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider _ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            _ordersProvider = ordersProvider;
        }


        [HttpGet("{customerID}")]
        public async Task<IActionResult> GetOrdersAsync(int customerID)
        {
           var result = await _ordersProvider.GetOrdersAsync(customerID);

           if (result.IsSuccess)
               return Ok(result.Orders);

           return NotFound();

        }
    }
}
