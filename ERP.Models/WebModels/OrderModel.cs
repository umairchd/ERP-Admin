using System.Collections.Generic;

namespace ERP.Models.WebModels
{
    public class OrderModel
    {
        public long OrderId { get; set; }
        public long ? CustomerId { get; set; }
        public bool IsModified { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string Comments { get; set; }
        public string[] CustomerIds { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }

        public List<OrderItemModelWithProduct> OrderItems { get; set; }
        public List<CustomerOrderModel> CustomerOrderModel { get; set; }
        


        public int AllowedMaxDiscount { get; set; }

        //public virtual Customer Customer { get; set; }
    }
}