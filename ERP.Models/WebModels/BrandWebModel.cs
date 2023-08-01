using System.ComponentModel.DataAnnotations;

namespace ERP.Models.WebModels
{
    public class BrandWebModel
    {
        public long BrandId { get; set; }
        [Display(Name="Brand Name")]
        public string BrandName { get; set; }
        [Display(Name = "Brand Description")]
        public string BrandDescription { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
    }
}
