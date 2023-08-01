namespace ERP.Models.WebModels
{
    public class ReturnedOrderItemModel
    {
        public long ReturnId { get; set; }
        public long OrderId { get; set; }
        public long ProductVariationId { get; set; }
        public int ReturnedQty { get; set; }
        public decimal Price { get; set; }
        public decimal ReturnedSubTotal { get; set; }
        public string ReceivedBy { get; set; }
        public System.DateTime ReceivedDate { get; set; }
        public decimal SalePrice { get; set; }
        public decimal SaleDiscount { get; set; }
        public decimal SaleSubTotal{ get; set; }
        public int SaleQty { get; set; }
        public decimal Deduction { get; set; }
    }

    public class ReturnItemLVModel
    {
        public long ReturnId { get; set; }
        public long OrderId { get; set; }
        public int ReturnedQty { get; set; }
        public string ReceivedBy { get; set; }
        public string ReturnedDate { get; set; }
        public long ItemCode { get; set; }
        public decimal Price { get; set; }
        public decimal NetTotal { get; set; }
    }
}