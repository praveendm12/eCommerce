using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Api.Products.db;
using eCommerce.Api.Products.Models;

namespace eCommerce.Api.Products.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<DbProduct, Product>();
        }
    }
}
