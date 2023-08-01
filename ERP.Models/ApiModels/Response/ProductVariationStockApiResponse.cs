namespace ERP.Models.ApiModels.Response
{
    public class ProductVariationStockApiResponse
    {
        public long ProductVariationId { get; set; }
        public string Barcode { get; set; }
        public long AvailableQuantity { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public string UnitTitle { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
    }
}
