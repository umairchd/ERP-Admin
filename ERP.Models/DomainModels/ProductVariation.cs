using System;
using System.Collections.Generic;

namespace ERP.Models.DomainModels
{
    public class ProductVariation
    {
        public long ProductVariationId { get; set; }
        public long ProductId { get; set; }
        public int? UnitId { get; set; }
        public string Barcode { get; set; }
        public decimal Purchaseprice { get; set; }
        public decimal Saleprice { get; set; }
        public int MinimumStockLimit { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public string ProductVariantDescription { get; set; }
        public long? ShelfId { get; set; }

        public virtual Unit Unit { get; set; }
        public virtual ICollection<PurchaseBillItem> PurchaseBillItems { get; set; }
        public virtual Product Product { get; set; }
        public virtual Shelf Shelf { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ReturnedOrderItem> ReturnedOrderItems { get; set; }

    }
}
