using System.Web.Http;
using ERP.Interfaces.IServices;
using ERP.Models.ApiModels.Response;

namespace ERP.Web.Areas.Api.Controllers
{
    public class ProductVariationStockController : ApiController
    {
        private readonly IProductVariationService productVariationService;

        public ProductVariationStockController(IProductVariationService productVariationService)
        {
            this.productVariationService = productVariationService;
        }

        public ProductVariationStockApiResponse Get(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var response = productVariationService.GetProductVariationStock(code);
                if (response != null)
                {
                    return response;
                }
            }
            return new ProductVariationStockApiResponse();
        }
    }
}