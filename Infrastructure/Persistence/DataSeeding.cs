using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                //Production
                if (PendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (!_dbContext.ProductBrands.Any())
                {
                    //var ProductBrandData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var ProductBrandData =  File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    //Covert Data "String" => C# Objects [ProductBrand]
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);

                    if (ProductBrands is not null && ProductBrands.Any())
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    //Covert Data "String" => C# Objects [ProductBrand]
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);

                    if (ProductTypes is not null && ProductTypes.Any())
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);

                }

                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    //Covert Data "String" => C# Objects [ProductBrand]
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);

                    if (Products is not null && Products.Any())
                        await _dbContext.Products.AddRangeAsync(Products);

                }
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
