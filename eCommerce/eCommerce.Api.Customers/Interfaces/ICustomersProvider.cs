using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool isSuccess, IEnumerable<Customer>)> GetCustomersAsync();

        Task<(bool isSuccess, Customer customer)> GetCustomerByIdAsync(int id);
    }
}
