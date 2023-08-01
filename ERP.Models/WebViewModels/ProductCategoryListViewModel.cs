using System.Collections.Generic;
using ERP.Models.RequestModels;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class ProductCategoryListViewModel
    {        
        public IEnumerable<ProductCategoryModel> data { get; set; }
        /// <summary>
        /// Search Request
        /// </summary>
        public ProductCategorySearchRequest SearchRequest { get; set; }

        public int recordsTotal;
        public int recordsFiltered;
    }
}