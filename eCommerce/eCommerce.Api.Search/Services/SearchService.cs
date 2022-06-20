using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Api.Search.Interfaces;

namespace eCommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _ordersService;
        private readonly IProductsService _productsService;
        private readonly ICustomerService _customerService;

        public SearchService(IOrdersService ordersService,
            IProductsService productsService,
            ICustomerService customerService)
        {
            _ordersService = ordersService;
            _productsService = productsService;
            _customerService = customerService;
        }

        public async Task<(bool IsSuccess, dynamic Result)> SearchAsync(int customerId)
        {
            var ordersResult = await _ordersService.GetOrdersAsync(customerId);

            var productResult = await _productsService.GetProductsAsync();

            var customerResult = await _customerService.GetCustomer(customerId);

            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productResult.IsSuccess
                            ? productResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name
                            : "Product Info not available";
                    }
                }

                var result = new
                {
                    Orders = ordersResult.Orders,
                    CustomerName= customerResult.Customer.Name
                };
                return (true, result);
            }

            return (false, null);
        }
    }
}
