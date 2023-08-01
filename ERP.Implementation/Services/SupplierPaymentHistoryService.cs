using System.Collections.Generic;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;

namespace ERP.Service.Services
{
    public class SupplierPaymentHistoryService : ISupplierPaymentHistoryService
    {
        private readonly ISupplierPaymentHistoryRepository supplierPaymentHistoryRepository;
        private readonly IVendorRepository vendorRepository;
        private readonly IPaymentMethodRepository paymentMethodRepository;
        private readonly ISupplierBankAccountRepository bankAccountRepository;
        private readonly IPurchaseBillRepository purchaseBillRepository;



        public SupplierPaymentHistoryService(ISupplierPaymentHistoryRepository supplierPaymentHistoryRepository, IVendorRepository vendorRepository, ISupplierBankAccountRepository bankAccountRepository, IPaymentMethodRepository paymentMethodRepository, IPurchaseBillRepository purchaseBillRepository)
        {
            this.supplierPaymentHistoryRepository = supplierPaymentHistoryRepository;
            this.vendorRepository = vendorRepository;
            this.bankAccountRepository = bankAccountRepository;
            this.paymentMethodRepository = paymentMethodRepository;
            this.purchaseBillRepository = purchaseBillRepository;
        }

        public IEnumerable<SupplierPaymentHistory> GetAllSuppliersPaymentHistories()
        {
            return supplierPaymentHistoryRepository.GetAll();
        }

        public SupplierPaymentHistorySearchResponse GetSupplierPaymentHistorySearchResponse(SupplierPaymentHistorySearchRequest searchRequest)
        {
            var response = supplierPaymentHistoryRepository.GetPaymentHistorySearchResponse(searchRequest);
            
            if (response.FilteredCount > 0 && searchRequest.Vendor != null && searchRequest.Vendor.Value > 0)
            {
                var sumOfPayment = response.PaymentHistoryList.Sum(x => x.Amount);
                var sumofBill = purchaseBillRepository.GetFilteredBills(searchRequest);
                var amount = sumOfPayment - sumofBill;
                if (amount >= 0)
                {
                    response.CreditAmount = amount;
                    response.BalanceAmount = 0;
                }
                else
                {
                    response.CreditAmount = 0;
                    response.BalanceAmount = amount * (-1);    
                }
                response.PaidAmount = sumOfPayment;

            }

            return response;

        }

        public SupplierPaymentHistoryBaseData GetPaymentHistoryBaseData(long ? id)
        {
            var baseData = new SupplierPaymentHistoryBaseData
            {
                PaymentMethods = paymentMethodRepository.GetAll(),
                SupplierBankAccounts = bankAccountRepository.GetAll().Where(x=>x.IsActive).ToList(),
                Vendors = vendorRepository.GetAll()
            };
            if (id != null)
                baseData.SupplierPaymentHistory = supplierPaymentHistoryRepository.Find((long) id);
            
            return baseData;

        }

        public bool SaveOrUpdate(SupplierPaymentHistory paymentHistory)
        {
            if(paymentHistory.SupplierPaymentHistoryId == 0)
                supplierPaymentHistoryRepository.Add(paymentHistory);
            else
                supplierPaymentHistoryRepository.Update(paymentHistory);

            supplierPaymentHistoryRepository.SaveChanges();
            return true;
        }
    }
}
