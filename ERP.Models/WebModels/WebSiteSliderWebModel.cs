using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ERP.Models.WebModels
{
    public class WebSiteSliderWebModel
    {
        public long SlideId { get; set; }

        [Display(Name = "Image Main Title")]
        public string MainTitle { get; set; }

        [Display(Name = "Image Subtitle")]
        public string SubTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageType { get; set; }

        [Display(Name = "Is Active")]
        public bool isActive { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public string SliderImageURL { get; set; }
        public HttpPostedFileBase SliderImage { get; set; }

    }
}
