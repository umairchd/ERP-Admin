namespace ERP.Models.DomainModels
{
    public class OrderItem
    {
        public long OrderItemId { get; set; }
        public int Quantity { get; set; }
        public long OrderId { get; set; }
        public long ProductVariationId { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual ProductVariation ProductVariation { get; set; }
    }
}
