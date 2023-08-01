using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;

namespace ERP.Interfaces.IRepository
{
    public interface ISupplierPaymentHistoryRepository : IBaseRepository<SupplierPaymentHistory, long>
    {
        SupplierPaymentHistorySearchResponse GetPaymentHistorySearchResponse(SupplierPaymentHistorySearchRequest searchRequest);

        SupplierPaymentHistory GetSupplierPaymentHistoriesBySupplierId(long purchaseBillId);
    }
}
