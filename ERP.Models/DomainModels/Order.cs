using System.Collections.Generic;

namespace ERP.Models.DomainModels
{
    public class Order
    {
        public long OrderId { get; set; }
        public bool IsModified { get; set; }
        public bool? IsDeleted { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string Comments { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
        public virtual ICollection<ReturnedOrderItem> ReturnedOrderItems { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
