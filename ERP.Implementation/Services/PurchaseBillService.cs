using System.Collections.Generic;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;
using ERP.Models.ModelMapers;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;
using ERP.Models.WebViewModels;

namespace ERP.Service.Services
{
    public class PurchaseBillService : IPurchaseBillService
    {
        private readonly IPurchaseBillRepository purchaseBillRepositoy;
        private readonly IVendorRepository vendorRepository;
        private readonly IPaymentMethodRepository paymentMethodRepository;
        private readonly ISupplierPaymentHistoryRepository supplierPaymentHistoryRepository;
        private readonly IProductVariationRepository productVariationRepository;
        private readonly ISupplierBankAccountRepository bankAccountRepository;

        public PurchaseBillService(IPurchaseBillRepository purchaseBillRepositoy,IVendorRepository vendorRepository, IPaymentMethodRepository paymentMethodRepository, ISupplierPaymentHistoryRepository supplierPaymentHistoryRepository, IProductVariationRepository productVariationRepository, ISupplierBankAccountRepository bankAccountRepository)
        {
            this.purchaseBillRepositoy = purchaseBillRepositoy;
            this.vendorRepository = vendorRepository;
            this.paymentMethodRepository = paymentMethodRepository;
            this.supplierPaymentHistoryRepository = supplierPaymentHistoryRepository;
            this.productVariationRepository = productVariationRepository;
            this.bankAccountRepository = bankAccountRepository;
        }

        public PurchaseBill GetPurchaseBill(long purchaseBillId)
        {
            return purchaseBillRepositoy.GetPurchaseBill(purchaseBillId);
        }

        public PurchaseBillViewModel GetPurchaseBillResponse(long? purchaseBillId)
        {
            var purchaseBillViewModel = new PurchaseBillViewModel();
            
            if (purchaseBillId != null)
            {
                var purchaseBill = purchaseBillRepositoy.GetPurchaseBill((long)purchaseBillId);
                if (purchaseBill != null)
                {
                    purchaseBillViewModel.PurchaseBill = purchaseBill.MapServerToClient();
                    purchaseBillViewModel.InventoryItems =
                        purchaseBill.PurchaseBillItems.Select(x => x.MapPurchaseBillItem()).ToList();
                    //Calculate SubTotal and GrossTotal
                    purchaseBillViewModel.SubTotal = purchaseBill.PurchaseBillItems.Sum(x => x.PurchasePrice * x.Quantity);
                    purchaseBillViewModel.GrossTotal = purchaseBillViewModel.SubTotal - purchaseBill.TotalDiscount;

                    if (purchaseBillViewModel.PurchaseBill.VendorId != null)
                        purchaseBillViewModel.SupplierAccountsDdl =
                            bankAccountRepository.GetAllBankAccountsByVendorId(
                                purchaseBillViewModel.PurchaseBill.VendorId.Value).Select(x=>x.MapDdlFromServerToClient()).ToList();

                    var paymentDetails =
                        supplierPaymentHistoryRepository.GetSupplierPaymentHistoriesBySupplierId((int) purchaseBillId);
                    if(paymentDetails!=null)
                        purchaseBillViewModel.SupplierPaymentHistory = paymentDetails.CreateFromServerToClient();

                }
            }
            purchaseBillViewModel.PaymentMethods = paymentMethodRepository.GetAll().ToList().Select(x => x.CreateFromServerToClient()).ToList();

            
            purchaseBillViewModel.Vendors =
                vendorRepository.GetActiveVendors().Select(x => x.MapVendorsDDL()).ToList();

            return purchaseBillViewModel;
        }

        public IEnumerable<PurchaseBill> GetAllPurchaseBills()
        {
            return purchaseBillRepositoy.GetAll();
        }

        public long AddPurchaseBill(PurchaseBill purchaseBill)
        {
            purchaseBillRepositoy.Add(purchaseBill);
            purchaseBillRepositoy.SaveChanges();
            return purchaseBill.PurchaseBillId;
        }

        public bool AddPurchaseBillWithPayment(PurchaseBillWithPayment purchaseBillWithPayment)
        {

            var purchaseBillId = AddPurchaseBill(purchaseBillWithPayment.PurchaseBill);
            
            UpdateProductVariationPrices(purchaseBillWithPayment);
            if(purchaseBillWithPayment.SupplierPaymentHistory.Amount > 0)
                AddPurchaseBillPayment(purchaseBillWithPayment, purchaseBillId);
            
            return true;
        }

        private void AddPurchaseBillPayment(PurchaseBillWithPayment purchaseBillWithPayment, long purchaseBillId)
        {
            if (purchaseBillId > 0)
            {
                purchaseBillWithPayment.SupplierPaymentHistory.PurchaseBillId = purchaseBillId;
                if (purchaseBillWithPayment.PurchaseBill.VendorId != null)
                    purchaseBillWithPayment.SupplierPaymentHistory.SupplierId =
                        purchaseBillWithPayment.PurchaseBill.VendorId.Value;
                supplierPaymentHistoryRepository.Add(purchaseBillWithPayment.SupplierPaymentHistory);
                supplierPaymentHistoryRepository.SaveChanges();
            }
        }

        private void UpdateProductVariationPrices(PurchaseBillWithPayment purchaseBillWithPayment)
        {
            foreach (var item in purchaseBillWithPayment.PurchaseBill.PurchaseBillItems)
            {
                var oldItem = productVariationRepository.Find(item.ProductVariationId);
                oldItem.Saleprice = item.SalePrice;
                oldItem.Purchaseprice = item.PurchasePrice;
                productVariationRepository.Update(oldItem);
            }
            productVariationRepository.SaveChanges();
        }

        public long UpdatePurchaseBill(PurchaseBill purchaseBill)
        {
            throw new System.NotImplementedException();
        }

        public PurchaseBillsSearchResponse SearchPurchaseBills(PuchaseBillsSearchRequest searchRequest)
        {
            return purchaseBillRepositoy.SearchPurchaseBills(searchRequest);
        }
    }
}

