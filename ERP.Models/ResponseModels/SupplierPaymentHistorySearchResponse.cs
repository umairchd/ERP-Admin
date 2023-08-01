using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Models.ResponseModels
{
    public class SupplierPaymentHistorySearchResponse
    {
        public SupplierPaymentHistorySearchResponse()
        {
            PaymentHistoryList = new List<SupplierPaymentHistory>();
        }

        /// <summary>
        /// Inventory Items
        /// </summary>
        public IEnumerable<SupplierPaymentHistory> PaymentHistoryList { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        
        public int TotalCount { get; set; }

        public int FilteredCount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal PaidAmount { get; set; }
    }

    public class SupplierPaymentHistoryBaseData
    {
        public SupplierPaymentHistoryBaseData()
        {
            Vendors = new List<Supplier>();
            SupplierBankAccounts = new List<SupplierBankAccount>();
            SupplierPaymentHistory = new SupplierPaymentHistory();
        }
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }
        public IEnumerable<Supplier> Vendors { get; set; }
        public IEnumerable<SupplierBankAccount> SupplierBankAccounts { get; set; }
        public SupplierPaymentHistory SupplierPaymentHistory { get; set; }
    }
}
