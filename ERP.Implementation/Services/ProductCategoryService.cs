using System.Collections.Generic;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;
using ERP.Models.ResponseModels;

namespace ERP.Service.Services
{
    public class ProductCategoryService:IProductCategoryService
    {
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IProductMainCategoryRepository productMainCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository,IProductMainCategoryRepository productMainCategoryRepository)
        {
            this.productCategoryRepository = productCategoryRepository;
            this.productMainCategoryRepository = productMainCategoryRepository;
        }

        public ProductCategory GetProductCategory(long productCategoryId)
        {
            return productCategoryRepository.Find(productCategoryId);
        }

        public CreateCategoryResponseModel GetProductCategoryResponse(long? productCategoryId)
        {
            var responseModel=new CreateCategoryResponseModel();
            if (productCategoryId != null)
            {
                responseModel.ProductCategory = productCategoryRepository.Find((long)productCategoryId);
            }
            responseModel.ProductMainCategories = productCategoryRepository.GetAllMainCategories().ToList();
            return responseModel;
        }

        public IEnumerable<ProductCategory> GetAllProductCategories()
        {
            return productCategoryRepository.GetAll().OrderBy(x=>x.Name).ToList();
        }

        public IEnumerable<ProductCategory> GetAllSubCategories()
        {
            return productCategoryRepository.GetAllSubCategories().ToList();
        }

        public long AddProductCategory(ProductCategory productCategory)
        {
            if (productCategory.CategoryId > 0)
                productCategoryRepository.Update(productCategory);
            else
                productCategoryRepository.Add(productCategory);
            
            productCategoryRepository.SaveChanges();
            return productCategory.CategoryId;
        }

        public IEnumerable<ProductCategory> GetAllSubCategoriesByMainCategoryId(long mainCategoryId)
        {
            return productCategoryRepository.GetAllSubCategoriesByMainCategoryId(mainCategoryId).ToList();
        }

        public IEnumerable<ProductCategory> GetAllMainCategories()
        {
            return productCategoryRepository.GetAllMainCategories().ToList();
        }
    }
}
