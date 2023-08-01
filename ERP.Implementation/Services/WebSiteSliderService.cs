using System.Collections.Generic;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
    class WebSiteSliderService:IWebSiteSliderService
    {
        private readonly IWebSiteSliderRepository webSiteSliderRepository;

        public WebSiteSliderService(IWebSiteSliderRepository webSiteSliderRepository)
        {
            this.webSiteSliderRepository = webSiteSliderRepository;
        }
        public IEnumerable<WebSiteSlider> GetAllSlides()
        {
            return webSiteSliderRepository.GetAll();
        }

        public WebSiteSlider GetSlide(long id)
        {
            return webSiteSliderRepository.Find(id);
        }

        public long AddSlide(WebSiteSlider slide)
        {
            if (slide.SlideId > 0)
                webSiteSliderRepository.Update(slide);
            else
                webSiteSliderRepository.Add(slide);

            webSiteSliderRepository.SaveChanges();
            return slide.SlideId;
        }
    }
}
