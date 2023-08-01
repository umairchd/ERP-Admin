using System.Collections.Generic;
using ERP.Models.ApiModels.Response;
using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;

namespace ERP.Interfaces.IRepository
{
    public interface IProductVariationRepository : IBaseRepository<ProductVariation, long>
    {
        ProductVariation GetProductByAnyCode(string code);
        IEnumerable<ProductVariation> GetShortOnStockProducts();
        IEnumerable<ProductVariation> GetProductVariationsByCategories(long mainCategoryId, long subCategoryId);
        ProductVariationSearchResponse GetProductVariationSearchResponse(ProductVariationSearchRequest searchRequest);
        ProductVariationStockApiResponse GetProductVariationStock(string code);
    }
}
