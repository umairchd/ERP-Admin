using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IRepository
{
    public interface ISupplierBankAccountRepository : IBaseRepository<SupplierBankAccount, long>
    {
         IEnumerable<SupplierBankAccount> GetAllBankAccounts();
        IEnumerable<SupplierBankAccount> GetAllBankAccountsByVendorId(long supllierId);
    }
}
