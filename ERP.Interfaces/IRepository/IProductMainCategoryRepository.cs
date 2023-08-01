using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IRepository
{
    public interface IProductMainCategoryRepository : IBaseRepository<ProductMainCategory, long>
    {
        IEnumerable<ProductMainCategory> ProductMainCategoriesWeb();
    }
}
