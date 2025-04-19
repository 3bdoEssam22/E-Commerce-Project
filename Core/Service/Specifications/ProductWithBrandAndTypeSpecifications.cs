using DomainLayer.Models;
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
        public ProductWithBrandAndTypeSpecifications(int? BrandId, int? TypeId, ProductSortingOptions sortingOption)
            : base(p => (!BrandId.HasValue || p.BrandId == BrandId) && (!TypeId.HasValue || p.TypeId == TypeId))
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);

            switch (sortingOption)
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
        }

        //Get Product By Id.
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }

}

