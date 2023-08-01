using System.Collections.Generic;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;
using ERP.Models.ModelMapers;
using ERP.Models.WebViewModels;

namespace ERP.Service.Services
{
    public class SupplierBankAccountService : ISupplierBankAccountService
    {
        private readonly ISupplierBankAccountRepository supplierBankAccountRepository;
        private readonly IVendorRepository vendorRepository;


        public SupplierBankAccountService(ISupplierBankAccountRepository supplierBankAccountRepository,IVendorRepository vendorRepository)
        {
            this.supplierBankAccountRepository = supplierBankAccountRepository;
            this.vendorRepository = vendorRepository;
        }

        public IEnumerable<SupplierBankAccount> GetAllSuppliersBankAccounts()
        {
            return supplierBankAccountRepository.GetAllBankAccounts();
        }
        public IEnumerable<SupplierBankAccount> GetAllBankAccountsByVendorId(long supplierId)
        {
            return supplierBankAccountRepository.GetAllBankAccountsByVendorId(supplierId);
        }

        public bool AddSuppliersBankAccount(SupplierBankAccount supplierBankAccount)
        {
            if (supplierBankAccount.SupplierBankAccountId > 0)
                supplierBankAccountRepository.Update(supplierBankAccount);
            else
                supplierBankAccountRepository.Add(supplierBankAccount);

            supplierBankAccountRepository.SaveChanges();
            return true;
        }

        public SupplierBankAccountViewModel GetSupplierBankAccount(long? id)
        {
            var supplierAccountViewModel = new SupplierBankAccountViewModel();
            if (id != null)
            {
                var supplierAccount= supplierBankAccountRepository.Find((long)id);
                if (supplierAccount != null)
                {
                    supplierAccountViewModel.supplierBankAccount = supplierAccount.CreateFromServerToClient();
                }
            }
            supplierAccountViewModel.Vendors =
                vendorRepository.GetActiveVendors().Select(x => x.MapVendorsDDL()).ToList();
            return supplierAccountViewModel;
        }
    }
}
