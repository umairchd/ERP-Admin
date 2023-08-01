using System;
using ERP.Models.Common;

namespace ERP.Models.RequestModels
{
    public class SupplierPaymentHistorySearchRequest : GetPagedListRequest
    {
        public string SupplierName { get; set; }
        public long? Vendor{ get; set; }
        public DateTime? PaymentDateFrom{ get; set; }
        public DateTime? PaymentDateTo { get; set; }

        public SupplierPaymentHistoryByColumn SupplierPaymentHistoryByColumn
        {
            get
            {
                return (SupplierPaymentHistoryByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
