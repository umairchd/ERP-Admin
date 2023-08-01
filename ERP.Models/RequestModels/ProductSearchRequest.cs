using ERP.Models.Common;

namespace ERP.Models.RequestModels
{
    public class ProductSearchRequest : GetPagedListRequest
    {
        public long ProductCode { get; set; }
        public string ProductVariationId { get; set; }
        public string ProductName { get; set; }
        public long CategoryId { get; set; }

        public ProductByColumn ProductOrderBy
        {
            get
            {
                return (ProductByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }

        public ProductSearchRequest()
        {
            SortBy = 0;
            IsAsc = false;
        }

    }

    public class ProductVariationSearchRequest : GetPagedListRequest
    {
        public long ProductCode { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public long Category { get; set; }

        public ProductVariationByColumn ProductOrderBy
        {
            get
            {
                return (ProductVariationByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }

        public ProductVariationSearchRequest()
        {
            SortBy = 0;
            IsAsc = false;
        }

    }
}
