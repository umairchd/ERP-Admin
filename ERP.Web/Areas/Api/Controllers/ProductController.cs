using System.Web.Http;
using ERP.Interfaces.IServices;
using ERP.Models.ModelMapers;
using ERP.Models.WebViewModels;

namespace ERP.Web.Areas.Api.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        // GET api/<controller>/5
        public ProductApiModel Get(string id)
        {
            if (string.IsNullOrEmpty(id)) return new ProductApiModel();

            var response = productService.GetProductByAnyCode(id);
            if (response.Product == null) return new ProductApiModel();

            var productApiModel = response.Product.CreateApiModelServerToClient();
            productApiModel.AvailableItems = response.AvailableItems;
            
            return productApiModel;
        }
    }
}