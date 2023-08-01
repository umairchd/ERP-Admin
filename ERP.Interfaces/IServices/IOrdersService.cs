using System.Collections.Generic;
using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;
using ERP.Models.WebModels;

namespace ERP.Interfaces.IServices
{
    public interface IOrdersService
    {
        Order GetOrders(long orderId);
        IEnumerable<Order> GetAllOrders();

        long AddService(Order order);
        long AddReturnItem(ReturnedOrderItem returnedOrderItem);
        bool UpdateService(Order order);
        bool DeleteOrder(long orderId);
        ReturnedOrderItemModel GetReturnedOrderItem(long returnId);
        OrderSearchResponse GetOrdersSearchResponse(OrderSearchRequest searchRequest);
        ReturnItemsSearchResponse GetReturnItemsSearchResponse(ReturnItemSearchRequest searchRequest);
    }
}
