using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Api.Orders.Db;
using eCommerce.Api.Orders.Models;

namespace eCommerce.Api.Orders.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<DbOrder, Order>();
            CreateMap<DbOrderItem, OrderItem>();
        }
    }
}
