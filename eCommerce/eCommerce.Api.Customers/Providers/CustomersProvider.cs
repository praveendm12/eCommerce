using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Api.Customers.db;
using eCommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomerDbContext _context;
        private readonly IMapper _mapper;

        public CustomersProvider(CustomerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_context.Customers.Any())
            {
                _context.Customers.Add(new DbCustomer() { Id = 1, Name = "Rama", Address = "Bengaluru" });
                _context.Customers.Add(new DbCustomer() { Id = 2, Name = "Lakshmana", Address = "Mysuru" });
                _context.Customers.Add(new DbCustomer() { Id = 3, Name = "Sita", Address = "Belagavi" });
                _context.Customers.Add(new DbCustomer() { Id = 4, Name = "Hanuman", Address = "Chikpete" });
                _context.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Customer>)> GetCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            if (customers != null && customers.Any())
            {
                return (true, _mapper.Map<IEnumerable<DbCustomer>, IEnumerable<Customer>>(customers));
            }

            return (false, null);
        }

        public async Task<(bool isSuccess, Customer customer)> GetCustomerByIdAsync(int id)
        {
            var customers = await _context.Customers.ToListAsync();
            if (customers != null && customers.Any())
            {
                var cust = customers.FirstOrDefault(item => item.Id == id);
                if (cust != null)
                    return (true, _mapper.Map<DbCustomer, Customer>(cust));
            }

            return (false, null);
        }
    }
}
