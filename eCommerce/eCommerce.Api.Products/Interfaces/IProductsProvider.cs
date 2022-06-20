using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Api.Products.Models;

namespace eCommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool isSuccess, IEnumerable<Product> products, string errorMessage)> GetProductsAsync();

        Task<(bool isSuccess, Product product, string errorMessage)> GetProductByIdAsync(int id);
    }
}
