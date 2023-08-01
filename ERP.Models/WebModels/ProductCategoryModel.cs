using System.ComponentModel.DataAnnotations;

namespace ERP.Models.WebModels
{
    public class ProductCategoryModel
    {
        public long CategoryId { get; set; }
        [Display(Name = "Main Category")]
        public long? MainCategoryId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Display(Name = "Name With Main Category")]
        public string NameWithMainCategory { get; set; }
        public string Description { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public string ProductMainCategoryName { get; set; }
    
        
    }
    public class ProductCategoryDDLModel
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string NameWithMainCategory { get; set; }
    }
}