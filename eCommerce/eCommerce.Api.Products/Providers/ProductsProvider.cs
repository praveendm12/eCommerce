using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Api.Products.db;
using eCommerce.Api.Products.Interfaces;
using eCommerce.Api.Products.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductDbContext _productDbContext;
        private readonly ILogger<ProductsProvider> _logger;
        private readonly IMapper _mapper;

        public ProductsProvider(ProductDbContext productDbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            _productDbContext = productDbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_productDbContext.Products.Any())
            {
                _productDbContext.Products.Add(new DbProduct() { Id = 1, Name = "Keyboard", Price = 500, Inventory = 100});
                _productDbContext.Products.Add(new DbProduct() { Id = 2, Name = "Mouse", Price = 10, Inventory = 200 });
                _productDbContext.Products.Add(new DbProduct() { Id = 3, Name = "Monitor", Price = 100, Inventory = 100 });
                _productDbContext.Products.Add(new DbProduct() { Id = 4, Name = "CPU", Price = 200, Inventory = 200 });
                _productDbContext.Products.Add(new DbProduct() { Id = 5, Name = "Webcam", Price = 5, Inventory = 100 });
                _productDbContext.SaveChanges();
            }
        }


        public async Task<(bool isSuccess, IEnumerable<Product> products, string errorMessage)> GetProductsAsync()
        {
            var products = await _productDbContext.Products.ToListAsync();
            if (products != null && products.Any())
            {
                return (true, _mapper.Map<IEnumerable<DbProduct>, IEnumerable<Product>>(products), "");
            }

            return (false, null, "Not Found");
        }

      


        public async Task<(bool isSuccess, Product product, string errorMessage)> GetProductByIdAsync(int id)
        {
            var product = await _productDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                return (true, _mapper.Map<DbProduct, Product>(product), "");
            }

            return (false, null, "Not Found");
        }
    }
}
