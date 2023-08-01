using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;

namespace ERP.Interfaces.IRepository
{
    public interface IPurchaseBillRepository : IBaseRepository<PurchaseBill, long>
    {
        PurchaseBill GetPurchaseBill(long purchaseBillId);
        PurchaseBillsSearchResponse SearchPurchaseBills(PuchaseBillsSearchRequest searchRequest);

        decimal GetFilteredBills(SupplierPaymentHistorySearchRequest searchRequest);
    }
}
