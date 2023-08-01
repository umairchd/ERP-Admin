using System.Collections.Generic;
using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;

namespace ERP.Interfaces.IServices
{
    public interface IInventoryItemService
    {
        PurchaseBillItem GetInventoryItem(long inventoryItemId);
        IEnumerable<PurchaseBillItem> GetAllInventoryItems();
        long AddInventoryItem(PurchaseBillItem inventoryItem);
        InventoryItemSearchResponse GetInventoryItemSearchResponse(InventoryItemSearchRequest searchRequest);
        InventoryItemResponse GetInventoryItemResponse(long? inventoryItemId);
    }
}
