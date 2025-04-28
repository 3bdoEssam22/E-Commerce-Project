using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext
            , UserManager<ApplicationUser> _userManager
            , RoleManager<IdentityRole> _roleManager
            , StoreIdentityDbContext _identityDbContext) : IDataSeeding
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
                    var ProductBrandData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
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
            catch (Exception)
            {

            }
        }

        public async Task IdentityDataSeedAsync()
        {

            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser
                    {
                        Email = "Abdo@gmail.com",
                        DisplayName = "Abdo Essam",
                        PhoneNumber = "01234567890",
                        UserName = "3bdox.x",
                    };

                    var User02 = new ApplicationUser
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Mohamed",
                        PhoneNumber = "011384546465",
                        UserName = "SalmaMohamed",
                    };

                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");

                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");
                }

                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

        }
    }
}
