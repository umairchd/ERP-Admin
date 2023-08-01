using System.Collections.Generic;
using ERP.Models.DomainModels;
using ERP.Models.ResponseModels;

namespace ERP.Interfaces.IServices
{
    public interface IProductCategoryService
    {
        ProductCategory GetProductCategory(long productCategoryId);
        CreateCategoryResponseModel GetProductCategoryResponse(long? productCategoryId);
        IEnumerable<ProductCategory> GetAllProductCategories();
        IEnumerable<ProductCategory> GetAllSubCategories();
        long AddProductCategory(ProductCategory productCategory);
        IEnumerable<ProductCategory> GetAllSubCategoriesByMainCategoryId(long mainCategoryId);
        IEnumerable<ProductCategory> GetAllMainCategories();
    }
}
