using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Models.ResponseModels
{
    public class CreateCategoryResponseModel
    {
        public ProductCategory ProductCategory { get; set; }
        public IEnumerable<ProductCategory> ProductMainCategories { get; set; }
    }
}
