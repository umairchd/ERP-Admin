namespace ERP.Models.WebModels
{
    public class OrderListViewModel
    {
        public long OrderId { get; set; }
        public string OrderDetailLink { get; set; }
        public string RecCreatedDate { get; set; }

        public string RecCreatedDateFormatted { get; set; }

        public System.DateTime RecLastUpdatedDate { get; set; }
        public string Comments { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }

        
        public double SubTotal { get; set; }
        public double TotalItems { get; set; }

        public decimal TotalDiscount { get; set; }
        public double NetTotal { get; set; }

        public string OrderPrintLink { get; set; }
        
        //Will only use at printing
        //public virtual Customer Customer { get; set; }
    }
}