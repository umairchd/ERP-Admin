using System.Collections.Generic;
using ERP.Models.RequestModels;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class InventoryItemsListViewModel
    {
        public InventoryItemsListViewModel()
        {
            Vendors = new List<VendorModel>();
        }
        public IEnumerable<InventoryItemListModel> data { get; set; }
        public IEnumerable<VendorModel> Vendors { get; set; }
        /// <summary>
        /// Search Request
        /// </summary>
        public InventoryItemSearchRequest SearchRequest { get; set; }

        public int recordsTotal;
        public int recordsFiltered;
    }
}