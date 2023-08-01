using System.Collections.Generic;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class ProductCategoryViewModel
    {
        public ProductCategoryViewModel()
        {
            ProductMainCategoriesDDL = new List<ProductCategoryDDLModel>();
        }
        public ProductCategoryModel ProductCategoryModel { get; set; }
        public IEnumerable<ProductCategoryDDLModel> ProductMainCategoriesDDL { get; set; }
    }
}