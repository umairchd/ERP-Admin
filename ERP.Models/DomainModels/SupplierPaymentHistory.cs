namespace ERP.Models.DomainModels
{
    public class SupplierPaymentHistory
    {
        public long SupplierPaymentHistoryId { get; set; }
        public long SupplierId { get; set; }
        public long ? SupplierBankAccountId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public long ? PurchaseBillId { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual SupplierBankAccount SupplierBankAccount { get; set; }
        public virtual PurchaseBill PurchaseBill { get; set; }
    }
}
