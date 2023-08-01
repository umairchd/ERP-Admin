using System.Collections.Generic;
using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;

namespace ERP.Interfaces.IRepository
{
    public interface IProductRepository : IBaseRepository<Product, long>
    {
        ProductSearchResponse GetProductSearchResponse(ProductSearchRequest searchRequest);
        //Product GetProductByAnyCode(string code);
        //Added by Umer--<       
        IEnumerable<Product> GetProductsById(long id);
        IEnumerable<Product> GetLatestProducts();
        IEnumerable<Product> GetProductsByCategories(long mainCategoryId, long subCategoryId);
        IEnumerable<Product> GetFeaturedProducts();
        //IEnumerable<Product> GetShortOnStockProducts();
        //-->
        WebsiteProductSearchResponse GetWebsiteProductSearchResponse(WebsiteProductSearchRequest searchRequest);
    }
}
