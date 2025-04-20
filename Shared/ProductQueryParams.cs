using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {
        private const int DefaultPagesize = 5;
        private const int MaxPagesize = 10;
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptions SortingOption { get; set; }
        public string? searchValue { get; set; }
        public int PageIndex { get; set; } = 1;

        private int pageSize = DefaultPagesize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPagesize ? MaxPagesize : value; }
        }

    }
}
