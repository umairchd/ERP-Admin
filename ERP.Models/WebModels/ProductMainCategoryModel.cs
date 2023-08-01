﻿using System.ComponentModel.DataAnnotations;

namespace ERP.Models.WebModels
{
    public class ProductMainCategoryModel
    {
        public long CategoryId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public string Icon { get; set; }
    }
}