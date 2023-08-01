using System.Collections.Generic;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class InventoryItemViewModel
    {
        public InventoryItemViewModel()
        {
            Vendors = new List<VendorModel>();
            Units = new List<UnitModel>();
            Shelf = new List<ShelfModel>();
        }
        public IEnumerable<VendorModel> Vendors { get; set; }
        //ADDED BY UMER
        public IEnumerable<UnitModel> Units { get; set; }
        public IEnumerable<ShelfModel> Shelf { get; set; } //
        public InventoryItemModel InventoryItem { get; set; }
    }
}