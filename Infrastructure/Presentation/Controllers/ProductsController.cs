using Microsoft.AspNetCore.Mvc;
using ServicesAbstracion;
using Shared;
using Shared.DataTransferObjects.ProductModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //BaseURL/api/products
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        //GetAllProducts
        //GET BaseURL/api/products
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDTo>>> GetAllProducts([FromQuery] ProductQueryParams queryParams)
        {
            var Products = await _serviceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(Products);
        }

        //GetProductById
        //GET BaseURL/api/products/10
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTo>> GetProductById(int id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }

        //GetAllBrands
        //GET BaseURL/api/products/brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTo>>> GetBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }

        //GetAllTypes
        //GET BaseURL/api/products/types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTo>>> GetTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }
    }
}