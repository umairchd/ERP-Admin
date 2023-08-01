using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Models.ResponseModels
{
    public sealed class InventoryItemResponse
    {
        public InventoryItemResponse()
        {
            Vendors = new List<Supplier>();
            Units = new List<Unit>();
            Shelf = new List<Shelf>();
        }

        /// <summary>
        /// Inventory Vendors
        /// </summary>
        public IEnumerable<Supplier> Vendors { get; set; }
        //ADDED BY UMER
        public IEnumerable<Unit> Units { get; set; }
        public IEnumerable<Shelf> Shelf { get; set; } //
        public PurchaseBillItem InventoryItem { get; set; }
    }
}
