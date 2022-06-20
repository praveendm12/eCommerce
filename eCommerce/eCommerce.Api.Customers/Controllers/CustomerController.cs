using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomersProvider _customersProvider;

        public CustomerController(ICustomersProvider customersProvider)
        {
            _customersProvider = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await _customersProvider.GetCustomersAsync();
            if (result.isSuccess)
                return Ok(result.Item2);

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomersByIdAsync(int id)
        {
            var result = await _customersProvider.GetCustomerByIdAsync(id);
            if (result.isSuccess)
                return Ok(result.Item2);

            return NotFound();
        }

    }
}
