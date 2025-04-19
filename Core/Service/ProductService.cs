using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Services.Specifications;
using ServicesAbstracion;
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

        public async Task<IEnumerable<ProductDTo>> GetAllProductsAsync()
        {   
            var specification = new ProductWithBrandAndTypeSpecifications();
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specification);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTo>>(products);
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
