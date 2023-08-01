using System.Collections.Generic;
using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class ProductConfigurationViewModel
    {        
        public IEnumerable<VendorModel> vendorModel { get; set; }
        /// <summary>
        /// Search Request
        /// </summary>


        public ProductConfiguration Configuration { get; set; }

        public string LicenseKey { get; set; }
    }
}