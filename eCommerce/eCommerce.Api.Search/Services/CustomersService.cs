using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using eCommerce.Api.Search.Interfaces;
using eCommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;

namespace eCommerce.Api.Search.Services
{
    public class CustomersService : ICustomerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(IHttpClientFactory httpClientFactory, ILogger<CustomersService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }


        public async Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomer(int customerId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CustomersService");
                var response = await client.GetAsync($"api/customer/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
                    var result = JsonSerializer.Deserialize<Customer>(content, options);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return (false, null, ex.Message);
            }

        }
    }
}
