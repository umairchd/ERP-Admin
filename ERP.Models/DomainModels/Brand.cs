using System.Collections.Generic;

namespace ERP.Models.DomainModels
{
    public class Brand
    {
        public long BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandDescription { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }

      
        public virtual ICollection<Product> Products { get; set; }
    }
}
