using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Api.Search.Interfaces;
using eCommerce.Api.Search.Services;
using Polly;

namespace eCommerce.Api.Search
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ICustomerService, CustomersService>();

            services.AddControllers();
            services.AddHttpClient("OrdersService",
                config => { config.BaseAddress = new Uri(Configuration["Services:Orders"]); });
            services.AddHttpClient("CustomersService",
                config => { config.BaseAddress = new Uri(Configuration["Services:Customers"]); });
            services.AddHttpClient("ProductsService",
                config => { config.BaseAddress = new Uri(Configuration["Services:Products"]); })
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, q => TimeSpan.FromMilliseconds(0.5)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
