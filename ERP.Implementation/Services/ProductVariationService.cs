using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.ApiModels.Response;

namespace ERP.Service.Services
{
    public class ProductVariationService : IProductVariationService
    {
        private readonly IProductVariationRepository productVariationRepository;

        public ProductVariationService(IProductVariationRepository productVariationRepository)
        {
            this.productVariationRepository = productVariationRepository;
        }

        public ProductVariationStockApiResponse GetProductVariationStock(string code)
        {
            return productVariationRepository.GetProductVariationStock(code);
        }
    }
}
