using System;
using System.Collections.Generic;

namespace ERP.Models.DomainModels
{
    public class ProductCategory
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public long? ParentCategoryId { get; set; }
        public bool IsParentCategory { get; set; }
        public bool IsWeb { get; set; }

        public virtual ICollection<ProductCategory> SubCategories { get; set; }
        public virtual ProductCategory MainCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
