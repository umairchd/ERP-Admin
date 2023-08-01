using System.Collections.Generic;
using ERP.Models.DropdownListModels;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class SupplierBankAccountViewModel
    {
        public SupplierBankAccountViewModel()
        {
            Vendors = new List<VendorsDdl>();
            supplierBankAccount = new SupplierBankAccountModel();
        }

        public IEnumerable<VendorsDdl> Vendors { get; set; }

        public SupplierBankAccountModel supplierBankAccount { get; set; }
        
    }
}