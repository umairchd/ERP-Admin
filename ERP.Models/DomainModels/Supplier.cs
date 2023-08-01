using System.Collections.Generic;

namespace ERP.Models.DomainModels
{
    public class Supplier
    {
        public long SupplierId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool ActiveFlag { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<SupplierBankAccount> SupplierBankAccounts { get; set; }
        public virtual ICollection<SupplierPaymentHistory> SupplierPaymentHistories { get; set; }
        public virtual ICollection<PurchaseBill> PurchaseBills { get; set; }
    }
}
