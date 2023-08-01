using System.Collections.Generic;
using ERP.Models.RequestModels;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class OrderViewModel
    {
        public OrderSearchRequest SearchRequest { get; set; }

        public List<OrderListViewModel> data { get; set; }

        public double GrossSale { get; set; }
        public decimal Discount{ get; set; }
        public double NetSale { get; set; }


        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int recordsTotal;

        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int recordsFiltered;
    }
}