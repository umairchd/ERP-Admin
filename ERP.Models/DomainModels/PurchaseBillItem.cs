namespace ERP.Models.DomainModels
{
    public class PurchaseBillItem
    {
        public long ItemId { get; set; }
        public long ProductVariationId { get; set; }
        public long Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public long PurchaseBillId { get; set; }
        public virtual ProductVariation ProductVariation { get; set; }
        public virtual PurchaseBill PurchaseBill { get; set; }
    }
}
