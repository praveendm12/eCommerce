using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider _productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            _productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await _productsProvider.GetProductsAsync();
            if (result.isSuccess)
            {
                return Ok(result.products);
            }

            return NotFound(result.errorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var result = await _productsProvider.GetProductByIdAsync(id);
            if (result.isSuccess)
            {
                return Ok(result.product);
            }

            return NotFound(result.errorMessage);
        }
    }
}
