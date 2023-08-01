namespace ERP.Models.WebModels
{
    public class ProductVariationModel
    {

        public long ProductVariationId { get; set; }
        public long ProductId { get; set; }
        public int? UnitId { get; set; }
        public long? ShelfId { get; set; }
        public string UnitTitle { get; set; }
        public string ShelfTitle { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public decimal Purchaseprice { get; set; }
        public decimal Saleprice { get; set; }
        public int MinimumStockLimit { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public string ProductVariantDescription { get; set; }
        public string ProductDefaultImageURL { get; set; }
        public bool HaveOtherImages { get; set; }
        
        //For List View Page
        public string CategoryTitle { get; set; }
        public string BrandTitle { get; set; }
        public string ProductDetails { get; set; }
        
    }
}