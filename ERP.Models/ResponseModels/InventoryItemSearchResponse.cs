using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Models.ResponseModels
{
    public sealed class InventoryItemSearchResponse
    {
        public InventoryItemSearchResponse()
        {
            InventoryItems = new List<PurchaseBillItem>();
        }

        /// <summary>
        /// Inventory Items
        /// </summary>
        public IEnumerable<PurchaseBillItem> InventoryItems { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        
        public int TotalCount { get; set; }

        public int FilteredCount { get; set; }

    }
}
