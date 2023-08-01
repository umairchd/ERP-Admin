using System.Collections.Generic;
using ERP.Models.RequestModels;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class WebsiteProductsListViewModel
    {
        public WebsiteProductsListViewModel()
        {
            SearchRequest = new WebsiteProductSearchRequest();
        }
        public IEnumerable<ProductWebApiModel> data { get; set; }
        /// <summary>
        /// Search Request
        /// </summary>
        public WebsiteProductSearchRequest SearchRequest { get; set; }

        public int recordsTotal;
        public int recordsFiltered;
    }
}