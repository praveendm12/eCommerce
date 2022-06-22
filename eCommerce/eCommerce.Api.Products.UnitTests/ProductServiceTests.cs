using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Api.Products.db;
using eCommerce.Api.Products.Profiles;
using eCommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace eCommerce.Api.Products.UnitTests
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts)).Options;
            var dbContext = new ProductDbContext(options);

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(config);

            var productProvider = new ProductsProvider(dbContext, null, mapper);

            var products = await productProvider.GetProductsAsync();
            Assert.True(products.isSuccess);
            Assert.True(products.products.Any());
            Assert.Empty(products.errorMessage);

        }

        [Fact]
        public async Task GetProductsWithIdReturnsValidProduct()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts)).Options;
            var dbContext = new ProductDbContext(options);

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(config);

            var productProvider = new ProductsProvider(dbContext, null, mapper);

            var products = await productProvider.GetProductByIdAsync(1);
            Assert.True(products.isSuccess);
            Assert.NotNull(products.product);
            Assert.Empty(products.errorMessage);

        }

        private void CreateProducts(ProductDbContext dbContext)
        {
            if (dbContext.Products.Any()) return;

            for (int i = 1; i <= 10; i++)
            {

                dbContext.Products.Add(new DbProduct()
                {
                    Id = i,
                    Inventory = i + 10,
                    Name = Guid.NewGuid().ToString(),
                    Price = (decimal) (i * 3)
                });
            }

            dbContext.SaveChanges();
        }
    }
}
