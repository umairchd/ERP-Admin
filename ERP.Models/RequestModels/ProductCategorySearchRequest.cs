using ERP.Models.Common;

namespace ERP.Models.RequestModels
{
    public class ProductCategorySearchRequest : GetPagedListRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public ProductCategoryByColumn ProductOrderBy
        {
            get
            {
                return (ProductCategoryByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
