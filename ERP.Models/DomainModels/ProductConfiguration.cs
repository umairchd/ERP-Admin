using System.ComponentModel.DataAnnotations;

namespace ERP.Models.DomainModels
{
    public class ProductConfiguration
    {
        public long Id { get; set; }
        [Range(0, 99, ErrorMessage = "Value must be less than 100.")]
        [Display(Name = "Max Allowed Discount")]
        public byte MaxAllowedDiscount { get; set; }
        [Display(Name = "Emails")]
        public string Emails { get; set; }
        [Display(Name = "Security Password")]
        public string SecurityPassword { get; set; }
        [Display(Name = "Product Code Prefix")]
        public string ProductCodePrefix { get; set; }
        public string Comments { get; set; }

        [Display(Name = "Default Supplier")]
        public long ? DefaultVendorId { get; set; }

        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }

    }
}
