using System.Collections.Generic;
using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;
using ERP.Models.WebViewModels;

namespace ERP.Interfaces.IServices
{
    public interface IPurchaseBillService
    {
        PurchaseBill GetPurchaseBill(long purchaseBillId);
        PurchaseBillViewModel GetPurchaseBillResponse(long? purchaseBillId);
        IEnumerable<PurchaseBill> GetAllPurchaseBills();
        long AddPurchaseBill(PurchaseBill purchaseBill);
        long UpdatePurchaseBill(PurchaseBill purchaseBill);
        PurchaseBillsSearchResponse SearchPurchaseBills(PuchaseBillsSearchRequest searchRequest);
        bool AddPurchaseBillWithPayment(PurchaseBillWithPayment purchaseBillWithPayment);
    }
}
