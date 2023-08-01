using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IRepository
{
    public interface IProductCategoryRepository : IBaseRepository<ProductCategory, long>
    {
        IEnumerable<ProductCategory> GetAllSubCategoriesByMainCategoryId(long mainCategoryId);
        IEnumerable<ProductCategory> GetAllSubCategories();
        IEnumerable<ProductCategory> GetAllMainCategories();
    }
}
