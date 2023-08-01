using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models.WebModels
{
    public class ProductModel
    {
        //
        [Display(Name = "Product ID")]
        public long ProductId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category")]
        public long CategoryId { get; set; }


        public string Comments { get; set; }

        [Display(Name = "Display this product on Web")]
        public bool IsWeb { get; set; }

        [Display(Name = "Description")]
        public string ProductDescription { get; set; }
        [Required]
        [Display(Name = "Brand")]
        public long? BrandId { get; set; }
        

        public DateTime RecCreatedDate { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }


        //ADDED BY USMAN
        public string CategoryName { get; set; } //To display on Listview
        //ADDED BY UMER
        public string BrandName { get; set; }
    }
}