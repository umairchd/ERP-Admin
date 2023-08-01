namespace ERP.Models.WebModels
{
    public class ProductWebApiModel
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal SalePrice { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public string Barcode { get; set; }

    }
}