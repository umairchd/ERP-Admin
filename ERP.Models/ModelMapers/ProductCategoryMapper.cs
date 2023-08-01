using System.Linq;
using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class ProductCategoryMapper
    {
        #region Product Category Mappers
        public static ProductCategory CreateFromClientToServer(this ProductCategoryModel source)
        {
            return new ProductCategory
            {
                CategoryId = source.CategoryId,
                Description = string.IsNullOrEmpty(source.Description) ? "" : source.Description,
                Name = source.Name,
                ParentCategoryId = source.MainCategoryId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }

        public static ProductCategoryModel CreateFromServerToClient(this ProductCategory source)
        {
            return new ProductCategoryModel
            {
                CategoryId = source.CategoryId,
                Description = source.Description,
                Name=source.Name,
                NameWithMainCategory = (source.MainCategory != null ? source.MainCategory.Name + " - " : "") + source.Name,
                MainCategoryId = source.ParentCategoryId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                ProductMainCategoryName = source.MainCategory==null ?"N/A": source.MainCategory.Name
            };
        }
        public static ProductCategoryDDLModel CreateForDDL(this ProductCategory source)
        {
            return new ProductCategoryDDLModel
            {
                CategoryId = source.CategoryId,
                Name = source.Name,
                NameWithMainCategory = source.Name+(source.MainCategory!=null?" - "+source.MainCategory.Name:""),
            };
        }
        #endregion

        #region Product Main-Category Mappers
        public static ProductMainCategory CreateFromClientToServer(this ProductMainCategoryModel source)
        {
            return new ProductMainCategory
            {
                CategoryId = source.CategoryId,
                Description = string.IsNullOrEmpty(source.Description) ? "" : source.Description,
                Name = source.Name,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }

        public static ProductMainCategoryModel CreateFromServerToClient(this ProductMainCategory source)
        {
            return new ProductMainCategoryModel
            {
                CategoryId = source.CategoryId,
                Description = source.Description,
                Name = source.Name,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }
        #endregion

        #region Api Mapper

        public static CategoryApiModel MapCategoryServerToClient(this ProductMainCategory productMainCategory)
        {
            return new CategoryApiModel
            {
                CategoryId = productMainCategory.CategoryId,
                Name = productMainCategory.Name,
                Icon = productMainCategory.Icon,
                SubCategories = productMainCategory.ProductCategories.Where(x=>x.IsWeb).Select(x=>x.MapSubCategoryServerToClient())
            };
        }
        public static SubCategoryApiModel MapSubCategoryServerToClient(this ProductCategory productCategory)
        {
            return new SubCategoryApiModel
            {
                CategoryId = productCategory.CategoryId,
                Name = productCategory.Name,
            };
        }
        #endregion
    }
}