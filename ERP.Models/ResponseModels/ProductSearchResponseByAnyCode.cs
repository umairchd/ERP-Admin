using ERP.Models.DomainModels;

namespace ERP.Models.ResponseModels
{
    public class ProductSearchResponseByAnyCode
    {
        //public Product Product { get; set; }
        public ProductVariation Product { get; set; }

        public long AvailableItems { get; set; }
    }
}
