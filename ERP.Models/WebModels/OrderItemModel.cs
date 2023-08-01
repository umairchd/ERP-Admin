

namespace ERP.Models.WebModels
{
    public class OrderItemModelWithProduct
    {
        public long OrderItemId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public long OrderId { get; set; }
        public decimal PurchasePrice { get; set; }
        //THIS IS THE TOTAL PRICE
        public decimal AmountGiven { get; set; }
        public decimal SalePrice { get; set; }
        public decimal MinSalePriceAllowed { get; set; }
        public decimal Discount { get; set; }
        public string Comments { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }

        public bool IsModified { get; set; }

        public decimal TotalItemAmount
        {
            get { return Quantity*SalePrice - Discount; }
        }
        //Calculated field
        public decimal Subtotal
        {
            get { return Quantity * SalePrice; }
        }
        public ProductVariationModel Product { get; set; }
    
    }
    public class OrderItemForApi
    {
        public long OrderItemId { get; set; }
        public long ProductVariationId { get; set; }
        public int Quantity { get; set; }
        public long OrderId { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalItemAmount
        {
            get { return Quantity * SalePrice - Discount; }
        }
        //Calculated field
        public decimal Subtotal
        {
            get { return Quantity * SalePrice; }
        }

    }
}