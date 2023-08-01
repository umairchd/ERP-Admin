namespace ERP.Models.WebModels
{
    public class PaymentMethodWebModel
    {
        public int PaymentMethodId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
    }
}