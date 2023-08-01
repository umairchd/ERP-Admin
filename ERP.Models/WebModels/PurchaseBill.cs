using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.WebModels
{
    public class PurchaseBill
    {
        public long PurchaseBillId { get; set; }
        [Display(Name = "Supplier")]
        public long? VendorId { get; set; }
        [Display(Name = "Supplier Inv.#")]
        public string VendorInvoiceNo { get; set; }
        public decimal TotalDiscount { get; set; }
        [Required]
        [Display(Name = "Payment")]
        public decimal PaidAmount { get; set; }
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
    }

    public class PurchaseBillHistory
    {
        public long BillId { get; set; }
        public string PurchaseDate { get; set; }
        public string SupplierName { get; set; }
        public string SupplierInvoiceNumber { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal Payment { get; set; }
    }
}
