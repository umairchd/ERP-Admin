using System.Collections.Generic;
using ERP.Models.DropdownListModels;
using ERP.Models.RequestModels;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class SupplierPaymentHistoryViewModel
    {
        public SupplierPaymentHistoryViewModel()
        {
            VendorsDdl = new List<VendorsDdl>();
            data = new List<SupplierPaymentHistoryWebModel>();
            SearchRequest = new SupplierPaymentHistorySearchRequest();
        }
        public IEnumerable<VendorsDdl> VendorsDdl { get; set; }
        public IEnumerable<SupplierPaymentHistoryWebModel> data { get; set; }
        public SupplierPaymentHistorySearchRequest SearchRequest { get; set; }

        public int recordsTotal;
        
        public int recordsFiltered;
        
        public decimal BalanceAmount;
        public decimal CreditAmount;
        public decimal PaidAmount;
    }
    public class SupplierPaymentHistoryCreateViewModel
    {
        public SupplierPaymentHistoryCreateViewModel()
        {
            VendorsDdl = new List<VendorsDdl>();
            PaymentHistoryWebModel = new SupplierPaymentHistoryModel();
            PaymentMethods = new List<PaymentMethodDdl>();
            SupplierBankAccounts = new List<SupplierBankAccountModel>();
        }
        public IEnumerable<VendorsDdl> VendorsDdl { get; set; }
        public SupplierPaymentHistoryModel PaymentHistoryWebModel { get; set; }
        public IEnumerable<PaymentMethodDdl> PaymentMethods { get; set; }
        public IEnumerable<SupplierBankAccountModel> SupplierBankAccounts { get; set; }
    }
}
