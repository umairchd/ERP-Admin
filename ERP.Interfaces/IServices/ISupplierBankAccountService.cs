using System.Collections.Generic;
using ERP.Models.DomainModels;
using ERP.Models.WebViewModels;

namespace ERP.Interfaces.IServices
{
    public interface ISupplierBankAccountService
    {
        
        IEnumerable<SupplierBankAccount> GetAllSuppliersBankAccounts();
        bool AddSuppliersBankAccount(SupplierBankAccount supplierBankAccount);

        SupplierBankAccountViewModel GetSupplierBankAccount(long? id);
        IEnumerable<SupplierBankAccount> GetAllBankAccountsByVendorId(long supplierId);

    }
}
