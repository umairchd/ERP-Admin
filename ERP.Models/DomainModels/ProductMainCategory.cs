﻿using System.Collections.Generic;

namespace ERP.Models.DomainModels
{
    public class ProductMainCategory
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public bool IsWeb { get; set; }
        public string Icon { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
