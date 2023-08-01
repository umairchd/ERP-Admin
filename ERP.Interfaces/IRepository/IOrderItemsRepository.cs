using System;
using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IRepository
{
    public interface IOrderItemsRepository : IBaseRepository<OrderItem, long>
    {
        long GetItemCountInOrders(long productId);
        IEnumerable<OrderItem> GetOrderItemsReport(string productCode, DateTime startDate, DateTime endDate);
        void GetDashboardWidgetsData(DateTime dateTime);
        OrderItem GetItemByProductVariationId(long productVariationId,long orderId);
    }
}
