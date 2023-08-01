using System.Collections.Generic;
using ERP.Models.RequestModels;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class ProductsListViewModel
    {
        public ProductsListViewModel()
        {
            ProductCategories = new List<ProductCategoryDDLModel>();
        }
        public IEnumerable<ProductModel> data { get; set; }
        public IEnumerable<ProductCategoryDDLModel> ProductCategories { get; set; }
        /// <summary>
        /// Search Request
        /// </summary>
        public ProductSearchRequest SearchRequest { get; set; }

        public int recordsTotal;
        public int recordsFiltered;
    }
}