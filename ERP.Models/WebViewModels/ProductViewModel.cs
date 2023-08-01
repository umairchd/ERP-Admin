using System.Collections.Generic;
using System.Web;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            ProductCategories = new List<ProductCategoryDDLModel>();
            ProductBrands = new List<BrandWebModel>();
            ProductVariations = new List<ProductVariationModel>();
            Units = new List<UnitModel>();
            Shelves = new List<ShelfModel>();
        }
        public ProductModel ProductModel { get; set; }
        public IEnumerable<ProductCategoryDDLModel> ProductCategories { get; set; }
        public IEnumerable<BrandWebModel> ProductBrands { get; set; }//ADDED BY UMER
        public IEnumerable<UnitModel> Units { get; set; }
        public IEnumerable<ShelfModel> Shelves { get; set; }
        public HttpPostedFileBase ProductDefaultImage { get; set; }
        public List<HttpPostedFileBase> ProductImages { get; set; }
        public List<ProductVariationModel> ProductVariations { get; set; }
    }
}