using System.Collections.Generic;
using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;

namespace ERP.Interfaces.IServices
{
    public interface ISupplierPaymentHistoryService
    {
        IEnumerable<SupplierPaymentHistory> GetAllSuppliersPaymentHistories();
        SupplierPaymentHistorySearchResponse GetSupplierPaymentHistorySearchResponse(SupplierPaymentHistorySearchRequest searchRequest);
        SupplierPaymentHistoryBaseData GetPaymentHistoryBaseData(long? id);
        bool SaveOrUpdate(SupplierPaymentHistory paymentHistory);
    }
}
