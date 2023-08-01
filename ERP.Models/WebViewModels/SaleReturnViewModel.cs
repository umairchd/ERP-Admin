using System.Collections.Generic;
using ERP.Models.RequestModels;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class SaleReturnViewModel
    {
        public SaleReturnViewModel()
        {
            SearchRequest = new ReturnItemSearchRequest();
            data = new List<ReturnItemLVModel>();
        }
        public ReturnItemSearchRequest SearchRequest { get; set; }
        public List<ReturnItemLVModel> data { get; set; }

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
