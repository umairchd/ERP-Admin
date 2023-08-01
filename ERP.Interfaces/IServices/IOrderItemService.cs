using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IServices
{
    public interface IOrderItemService
    {
        IEnumerable<OrderItem> GetOrderItems(long orderId);

        OrderItem GetOrderItemById(long orderItemId);
        
        IEnumerable<OrderItem> GetAllOrderItems();

        long AddService(OrderItem order);
        bool UpdateService(OrderItem order);
        void AddUpdateService(Order order);
        OrderItem LoadOrderItemByProductVariationId(string productVariationId, long orderId);
    }
}
