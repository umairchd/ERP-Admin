using System;
using System.Collections.Generic;
using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;

namespace ERP.Interfaces.IRepository
{
    public interface IPurchaseBillItemRepositoy : IBaseRepository<PurchaseBillItem, long>
    {
        long GetItemCountInInventory(long productId);
        InventoryItemSearchResponse GetInventoryItemSearchResponse(InventoryItemSearchRequest searchRequest);
        IEnumerable<PurchaseBillItem> PurchaseReport(string productCode, long vendorId, DateTime startDate, DateTime endDate);
    }
}
