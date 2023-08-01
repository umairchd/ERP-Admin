using System;
using ERP.Models.Common;

namespace ERP.Models.RequestModels
{
    public class PuchaseBillsSearchRequest : GetPagedListRequest
    {
        public PuchaseBillsSearchRequest()
        {
            IsAsc = false;
            SortBy = 0;
        }
        public string SupplierName { get; set; }
        //public string Barcode { get; set; }
        //public long ProductCode{ get; set; }
        public long? Vendor{ get; set; }
        public DateTime? PurchaseDateFrom { get; set; }
        public DateTime? PurchaseDateTo { get; set; }

        public PurchaseBillByColumn PurchaseBillByColumn
        {
            get
            {
                return (PurchaseBillByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
