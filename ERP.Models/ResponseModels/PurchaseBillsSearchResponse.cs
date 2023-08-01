using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Models.ResponseModels
{
    public sealed class PurchaseBillsSearchResponse
    {
        public PurchaseBillsSearchResponse()
        {
            PurchaseBills = new List<PurchaseBill>();
        }

        /// <summary>
        /// Inventory Items
        /// </summary>
        public IEnumerable<PurchaseBill> PurchaseBills { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        
        public int TotalCount { get; set; }

        public int FilteredCount { get; set; }

    }

    public class PurchaseBillWithPayment
    {
        public SupplierPaymentHistory SupplierPaymentHistory { get; set; }
        public PurchaseBill PurchaseBill { get; set; }
    }
}
