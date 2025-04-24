using DomainLayer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        //Get All Products with Brand and Type.
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams)
            : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
            && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
            && (string.IsNullOrEmpty(queryParams.searchValue) || (p.Name.ToLower().Contains(queryParams.searchValue))))
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);

            switch (queryParams.SortingOption)
            {
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                default:
                    break;
            }

            ApplyPagination(queryParams.PageSize , queryParams.PageIndex);
        }

        //Get Product By Id.
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }

}

