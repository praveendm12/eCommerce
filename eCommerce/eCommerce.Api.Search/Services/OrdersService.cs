﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using eCommerce.Api.Search.Interfaces;
using eCommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;

namespace eCommerce.Api.Search.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OrdersService> _logger;

        public OrdersService(IHttpClientFactory httpClientFactory, ILogger<OrdersService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("OrdersService");
                var response = await client.GetAsync($"api/orders/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);
                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
