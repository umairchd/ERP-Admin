namespace ERP.Models.DomainModels
{
    public class WebSiteSlider
    {
        public long SlideId { get; set; }
        public string MainTitle { get; set; }
        public string SubTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageType { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
    }
}
