using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IServices
{
    public interface IWebSiteSliderService
    {
        IEnumerable<WebSiteSlider> GetAllSlides();
        WebSiteSlider GetSlide(long id);
        long AddSlide(WebSiteSlider slide); 
    }
}