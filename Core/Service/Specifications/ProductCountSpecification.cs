using DomainLayer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    class ProductCountSpecification : BaseSpecifications<Product, int>

    {
        public ProductCountSpecification(ProductQueryParams queryParams)
            : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
            && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
            && (string.IsNullOrEmpty(queryParams.searchValue) || (p.Name.ToLower().Contains(queryParams.searchValue))))
        {

        }
    }
}
