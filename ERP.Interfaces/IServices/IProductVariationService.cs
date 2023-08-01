using ERP.Models.ApiModels.Response;

namespace ERP.Interfaces.IServices
{
    public interface IProductVariationService
    {
        ProductVariationStockApiResponse GetProductVariationStock(string code);
    }
}
