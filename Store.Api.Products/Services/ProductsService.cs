using Store.Api.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Api.Products.Models;
using AutoMapper;

namespace Store.Api.Products.Services
{
    public class ProductsService : IProductsService
    {
        private readonly Db.ProductsDbContext dbContext;
        private readonly ILogger<ProductsService> logger;
        private readonly IMapper mapper;

        public ProductsService(Db.ProductsDbContext dbContext, ILogger<ProductsService> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product() { Id = 1, Name = "Zapatos", Price = 20, Inventory = 100 });
                dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Sombrero", Price = 5, Inventory = 200 });
                dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Camisa", Price = 150, Inventory = 1000 });
                dbContext.Products.Add(new Db.Product() { Id = 4, Name = "Pantalón", Price = 200, Inventory = 2000 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                logger?.LogInformation($"Querying products with id: {id}");
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    logger?.LogInformation("Product found");
                    var result = mapper.Map<Product>(product);
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

        public async Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                logger?.LogInformation("Querying products");
                var products = await dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    logger?.LogInformation($"{products.Count} product(s) found");
                    var result = mapper.Map<IEnumerable<Product>>(products);
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