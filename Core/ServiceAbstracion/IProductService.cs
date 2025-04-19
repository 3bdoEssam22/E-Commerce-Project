using Shared;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstracion
{
    public interface IProductService
    {
        //Get All products
        Task<IEnumerable<ProductDTo>> GetAllProductsAsync(ProductQueryParams queryParams);

        //Get Product By Id
        Task<ProductDTo> GetProductByIdAsync(int id);

        //Get All Brands
        Task<IEnumerable<BrandDTo>> GetAllBrandsAsync();

        //Get All Types
        Task<IEnumerable<TypeDTo>> GetAllTypesAsync();

    }
}
