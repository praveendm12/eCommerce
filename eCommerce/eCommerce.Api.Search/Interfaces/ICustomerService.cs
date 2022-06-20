using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Api.Search.Models;

namespace eCommerce.Api.Search.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool IsSuccess, Customer Customer, string ErrorMessage)>
            GetCustomer(int customerId);
    }
}
