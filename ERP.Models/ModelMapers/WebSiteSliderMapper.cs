using System.Configuration;
using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class WebSiteSliderMapper
    {
        public static WebSiteSlider MapSlideClientToServer(this WebSiteSliderWebModel webSiteSliderWebModel)
        {
            return new WebSiteSlider()
            {
                SlideId = webSiteSliderWebModel.SlideId,
                ImageData = webSiteSliderWebModel.ImageData,
                ImageType = webSiteSliderWebModel.ImageType,
                MainTitle = webSiteSliderWebModel.MainTitle,
                SubTitle = webSiteSliderWebModel.SubTitle,
                IsActive = webSiteSliderWebModel.isActive,
                RecCreatedBy = webSiteSliderWebModel.RecCreatedBy,
                RecLastUpdatedDate = webSiteSliderWebModel.RecLastUpdatedDate,
                RecCreatedDate = webSiteSliderWebModel.RecCreatedDate,
                RecLastUpdatedBy = webSiteSliderWebModel.RecLastUpdatedBy,
            };
        }

        public static WebSiteSliderWebModel MapSlideServerToClient(this WebSiteSlider webSiteSlider)
        {
            return new WebSiteSliderWebModel()
            {
                ImageData = webSiteSlider.ImageData,
                SubTitle = webSiteSlider.SubTitle,
                ImageType = webSiteSlider.ImageType,
                MainTitle = webSiteSlider.MainTitle,
                isActive = webSiteSlider.IsActive,
                SlideId = webSiteSlider.SlideId,
                RecLastUpdatedDate = webSiteSlider.RecLastUpdatedDate,
                RecCreatedBy = webSiteSlider.RecCreatedBy,
                RecCreatedDate = webSiteSlider.RecCreatedDate,
                RecLastUpdatedBy = webSiteSlider.RecLastUpdatedBy,
                SliderImageURL = ImageURL(webSiteSlider)
            };
        }

        public static string ImageURL(WebSiteSlider slide)
        {
            string URL = string.Empty;
            if (slide.IsActive.Equals(true))
            {
                URL = ConfigurationManager.AppSettings["HostURL"] + ConfigurationManager.AppSettings["SliderImageUrl"] + slide.SlideId;
            }
            return URL;
        }
    }
}
