using AutoMapper;
using Store.Api.Customers.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Api.Customers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Api.Customers.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersService> logger;
        private readonly IMapper mapper;

        public CustomersService(CustomersDbContext dbContext, ILogger<CustomersService> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Customer() { Id = 1, Name = "Rodrigo", Address = "México" });
                dbContext.Customers.Add(new Customer() { Id = 2, Name = "Leonardo", Address = "Argentina" });
                dbContext.Customers.Add(new Customer() { Id = 3, Name = "Renata", Address = "España" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                logger?.LogInformation("Querying customers");
                var customers = await dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    logger?.LogInformation($"{customers.Count} customer(s) found");
                    var result = mapper.Map<IEnumerable<Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                logger?.LogInformation("Querying customers");
                var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if (customer != null)
                {
                    logger?.LogInformation("Customer found");
                    var result = mapper.Map<Customer, Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}