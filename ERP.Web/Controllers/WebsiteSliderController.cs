using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using ERP.Interfaces.IServices;
using ERP.Models.ModelMapers;
using ERP.Models.WebModels;
using ERP.Models.WebViewModels;

namespace ERP.Web.Controllers
{
    public class WebsiteSliderController : Controller
    {
        // GET: WebsiteSlider
        private readonly IWebSiteSliderService webSiteSliderService;
        public WebsiteSliderController(IWebSiteSliderService webSiteSliderService)
        {
            this.webSiteSliderService = webSiteSliderService;
        }

        public ActionResult Index()
        {
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            IEnumerable<WebSiteSliderWebModel> model = webSiteSliderService.GetAllSlides().Select(x => x.MapSlideServerToClient());
            return View(model);
        }

        public ActionResult Create(long? id)
        {
            WebSiteSliderWebModel model = new WebSiteSliderWebModel();
            if (id != null)
            {
                var slide = webSiteSliderService.GetSlide((long)id);
                if (slide != null)
                    model = slide.MapSlideServerToClient();
            }
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(WebSiteSliderWebModel webSiteSliderWebModel)
        {
            try
            {
                bool isCreated = false;
                if (webSiteSliderWebModel.SlideId == 0)
                {
                    webSiteSliderWebModel.RecCreatedBy = User.Identity.Name;
                    webSiteSliderWebModel.RecCreatedDate = DateTime.Now;
                    isCreated = true;
                }
                webSiteSliderWebModel.RecLastUpdatedBy = User.Identity.Name;
                webSiteSliderWebModel.RecLastUpdatedDate = DateTime.Now;

                if (webSiteSliderWebModel.ImageType != null && webSiteSliderWebModel.ImageData != null)
                {
                    if (webSiteSliderWebModel.SliderImage != null)
                    {
                        if (!SaveProductImage(webSiteSliderWebModel))
                        {
                            ViewBag.MessageVM = new MessageViewModel
                            {
                                Message = "Slider image size is inappropriate.",
                                IsError = true
                            };
                        }
                    }
                    //Slide Updated in DB
                    webSiteSliderService.AddSlide(webSiteSliderWebModel.MapSlideClientToServer());
                    TempData["message"] = new MessageViewModel { Message = "Slide has been updated successfully", IsUpdated = true };
                }
                else
                {
                    if (webSiteSliderWebModel.SliderImage != null && SaveProductImage(webSiteSliderWebModel))
                    {
                        //Save Slide to Db
                        webSiteSliderService.AddSlide(webSiteSliderWebModel.MapSlideClientToServer());
                        TempData["message"] = new MessageViewModel { Message = "Slide has been saved successfully",IsSaved = true };
                    }
                    else
                    {
                        ViewBag.MessageVM = new MessageViewModel
                        {
                            Message = "Slider image size is inappropriate.",
                            IsError = true
                        };
                        return View(webSiteSliderWebModel);
                    }
                }
                //if Save button is clicked
                if (Request.Form["save"] != null)
                    return RedirectToAction("Index");
                
                return RedirectToAction("Create");
            }
            catch (Exception exception)
            {
                ViewBag.MessageVM = new MessageViewModel { Message = "Oops, there is some problem.", IsError = true };
                return View(webSiteSliderWebModel);
            }
        }

        public FileResult LoadSliderImage(long id)
        {
            //pass id to service, and load image data
            var image = webSiteSliderService.GetSlide(id);

            string ext = image.ImageType.Split('/')[1];
            return File(image.ImageData, image.ImageType, "IMG_" + image.SlideId + ((DateTime)image.RecLastUpdatedDate).ToString("yyyyMMdd_HHmmss") + "." + ext);
        }

        private bool SaveProductImage(WebSiteSliderWebModel webSiteSliderWebModel)
        {
            var tempStream = webSiteSliderWebModel.SliderImage.InputStream;

            //File size must be less than 2MBs
            if (webSiteSliderWebModel.SliderImage.ContentLength > 0 &&
                webSiteSliderWebModel.SliderImage.ContentLength < 2000000)
            {
                //reisze the image for facebook optimization
                var resizedImage = Utility.ResizeImage(Image.FromStream(tempStream),
                    Utility.GetImageFormat(webSiteSliderWebModel.SliderImage.ContentType),
                    Convert.ToInt32(ConfigurationManager.AppSettings["ProductImageWidth"]),
                    Convert.ToInt32(ConfigurationManager.AppSettings["ProductImageHeight"]), true);

                byte[] bytes = new byte[resizedImage.Length];
                //copy file content to array
                resizedImage.Read(bytes, 0, Convert.ToInt32(resizedImage.Length));

                webSiteSliderWebModel.ImageData = bytes;
                webSiteSliderWebModel.ImageType = webSiteSliderWebModel.SliderImage.ContentType;
                return true;
            }
            return false;
        }
    }
}