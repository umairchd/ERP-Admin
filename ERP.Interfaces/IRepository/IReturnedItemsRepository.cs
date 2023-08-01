using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;

namespace ERP.Interfaces.IRepository
{
    public interface IReturnedItemRepository : IBaseRepository<ReturnedOrderItem, long>
    {
        ReturnItemsSearchResponse GetReturnItemsSearchResponse(ReturnItemSearchRequest searchRequest);
        ReturnedOrderItem GetReturnedOrderItem(long returnId);
    }
}
