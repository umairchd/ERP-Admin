namespace ERP.Models.WebModels
{
    public class SupplierPaymentHistoryModel
    {
        public long SupplierPaymentHistoryId { get; set; }
        public long SupplierId { get; set; }
        public long? SupplierBankAccountId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
    }
    public class SupplierPaymentHistoryWebModel
    {
        public long SupplierPaymentHistoryId { get; set; }
        public string Supplier { get; set; }
        public string SupplierBankAccountNumber { get; set; }
        public string SupplierBankAccountTitle { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentDate { get; set; }
    }
}
