namespace ERP.Models.DomainModels
{
    public partial class ReturnedOrderItem
    {
        public long ReturnId { get; set; }
        public long OrderId { get; set; }
        public long ProductVariationId { get; set; }
        public int ReturnedQty { get; set; }
        public string ReceivedBy { get; set; }
        public System.DateTime ReceivedDate { get; set; }
        public decimal Price { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Order Order { get; set; }
        public virtual ProductVariation ProductVariation { get; set; }
       
    }
}
