using System.Collections.Generic;
using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;

namespace ERP.Interfaces.IServices
{
    public interface IProductService
    {
        Product GetProduct(long productId);
        ProductVariation GetProductVariation(long productVariationId);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<ProductVariation> GetAllProductVariations();
        long SaveProduct(Product product);
        long GetAvailableProductItem(long productId);
        ProductSearchResponse GetProductSearchResponse(ProductSearchRequest searchRequest);
        ProductVariationSearchResponse GetProductVariationSearchResponse(ProductVariationSearchRequest searchRequest);
        WebsiteProductSearchResponse GetWebsiteProductSearchResponse(WebsiteProductSearchRequest searchRequest);
        ProductResponse GetProductResponse(long? productId);
        ProductSearchResponseByAnyCode GetProductByAnyCode(string code);

        //Added by Umer--<
        IEnumerable<Product> GetProductsById(long id);
        IEnumerable<Product> GetProductsByCategories(long mainCategoryId, long subCategoryId);
        IEnumerable<ProductVariation> GetProductVariationsByCategories(long mainCategoryId, long subCategoryId);
        IEnumerable<Product> GetLatestProducts();
        IEnumerable<Product> GetFeaturedProducts();
        //-->

        //Added by Jahangir
        bool DeleteProductVariation(long id);
    }
}
