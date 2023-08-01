namespace ERP.Models.DomainModels
{
    public class CustomerOrder
    {
        public long CustomerId { get; set; }
        public long OrderId { get; set; }
        public string Comments { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Order Order { get; set; }
    }
}
