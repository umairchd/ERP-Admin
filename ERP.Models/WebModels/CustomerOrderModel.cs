namespace ERP.Models.WebModels
{
    public class CustomerOrderModel
    {
        public long CustomerId { get; set; }
        public long OrderId { get; set; }
        public string Comments { get; set; }
    }
}
