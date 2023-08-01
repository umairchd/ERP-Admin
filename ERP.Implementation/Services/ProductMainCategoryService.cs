using System.Collections.Generic;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
    public class ProductMainCategoryService:IProductMainCategoryService
    {
        private IProductMainCategoryRepository productMainCategoryRepository;


        public ProductMainCategoryService(IProductMainCategoryRepository productMainCategoryRepository)
        {
            this.productMainCategoryRepository = productMainCategoryRepository;
        }


        public IEnumerable<ProductMainCategory> GetAllProductMainCategoriesWeb()
        {
            var mainCategories = productMainCategoryRepository.ProductMainCategoriesWeb();
            
            
            return mainCategories;
        }
    }
}
