using System;
using ERP.Models.Common;

namespace ERP.Models.RequestModels
{
    public class InventoryItemSearchRequest : GetPagedListRequest
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public long ProductCode{ get; set; }
        public long? Vendor{ get; set; }
        public DateTime? PurchaseDate { get; set; }

        public InventoryItemByColumn ItemOrderBy
        {
            get
            {
                return (InventoryItemByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
        public InventoryItemSearchRequest()
        {
            SortBy = 0;
            IsAsc = false;
        }
    }
}
