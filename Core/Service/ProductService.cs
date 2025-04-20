using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Services.Specifications;
using ServicesAbstracion;
using Shared;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDTo>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<BrandDTo>>(Brands);
            return BrandsDto;
        }

        public async Task<PaginatedResult<ProductDTo>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var specification = new ProductWithBrandAndTypeSpecifications(queryParams);
            var products = await Repo.GetAllAsync(specification);
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTo>>(products);
            var ProductCount = products.Count();
            var CountSpecification = new ProductCountSpecification(queryParams);
            var TotalCount = await Repo.CountAsync(CountSpecification);
            return new PaginatedResult<ProductDTo>(queryParams.PageIndex, ProductCount, TotalCount, Data);
        }

        public async Task<IEnumerable<TypeDTo>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTo>>(Types);
        }

        public async Task<ProductDTo> GetProductByIdAsync(int id)
        {
            var specification = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specification);
            return _mapper.Map<Product, ProductDTo>(product);
        }
    }
}
