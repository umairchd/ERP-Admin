namespace ERP.Models.WebModels
{
    public class SupplierBankAccountModel
    {
        public long SupplierBankAccountId { get; set; }
        public long SupplierId { get; set; }
        public string AccountTitle { get; set; }
        public string AccountNo { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
    }
    public class SupplierBankAccountListModel
    {
        public long SupplierBankAccountId { get; set; }
        public string AccountTitle { get; set; }
        public string AccountNo { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Supplier { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
    }
    
    
}
