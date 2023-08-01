using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;

namespace ERP.Interfaces.IRepository
{
    public interface IOrdersRepository : IBaseRepository<Order, long>
    {
        OrderSearchResponse GetOrdersSearchResponse(OrderSearchRequest searchRequest);
    }
}
