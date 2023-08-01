using System.Collections.Generic;

namespace ERP.Models.DomainModels
{
    public class Unit
    {
        public int UnitId { get; set; }
        public string UnitTitle { get; set; }
        public int UnitValue { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }

        public virtual ICollection<PurchaseBillItem> InventoryItems { get; set; }
        public virtual ICollection<ProductVariation> ProductVariations { get; set; }
    }
}
