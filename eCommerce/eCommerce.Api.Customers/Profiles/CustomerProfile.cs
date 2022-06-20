using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Api.Customers.db;

namespace eCommerce.Api.Customers.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<DbCustomer, Customer>();
        }
    }
}
