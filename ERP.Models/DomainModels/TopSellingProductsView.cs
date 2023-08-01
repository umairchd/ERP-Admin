namespace ERP.Models.DomainModels
{
    public class TopSellingProductsView
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public string ProductDescription { get; set; }
        public decimal SalePrice { get; set; }
        public long ImageId { get; set; }
        public bool isweb { get; set; }
        public int saleQuantity { get; set; }
    }
}
