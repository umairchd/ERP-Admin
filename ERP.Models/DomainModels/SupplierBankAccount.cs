using System.Collections.Generic;

namespace ERP.Models.DomainModels
{
    public class SupplierBankAccount
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

        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<SupplierPaymentHistory> SupplierPaymentHistories { get; set; }
    }
}
