using System;
using System.Collections.Generic;

namespace ERP.Models.DomainModels
{
    public class PurchaseBill
    {
        public PurchaseBill()
        {
            PurchaseBillItems = new HashSet<PurchaseBillItem>();
            SupplierPaymentHistories = new HashSet<SupplierPaymentHistory>();
        }
        public long PurchaseBillId { get; set; }
        public long? VendorId { get; set; }
        public string VendorInvoiceNo { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<PurchaseBillItem> PurchaseBillItems { get; set; }
        public virtual ICollection<SupplierPaymentHistory> SupplierPaymentHistories { get; set; }
    }
}
