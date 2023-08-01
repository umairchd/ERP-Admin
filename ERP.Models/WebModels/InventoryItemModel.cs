namespace ERP.Models.WebModels
{
    public class InventoryItemModel
    {
        public long ItemId { get; set; }
        public long ProductVariationId { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public string UnitTitle { get; set; }
        public long Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public long PurchaseBillId { get; set; }
    }
}