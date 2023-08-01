using System.Collections.Generic;

namespace ERP.Models.DomainModels
{
    public class Product
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public long CategoryId { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public string Comments { get; set; }
        public bool IsWeb { get; set; }
        public string ProductDescription { get; set; }
        public long? BrandId { get; set; }

        public virtual Brand Brand { get; set; }
        
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductVariation> ProductVariations { get; set; }
    }
}
