using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ERP.Interfaces.IServices;
using ERP.Models.ModelMapers;
using ERP.Models.WebModels;
using ERP.Models.WebViewModels;

namespace ERP.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        // GET: Brand
        public ActionResult Index()
        {
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            IEnumerable<BrandWebModel> brands = brandService.GetAllBrands().Select(x => x.CreateFromServerToClient());
            return View(brands);
           
        }

        
        public ActionResult Create(long? id)
        {
            BrandWebModel model = new BrandWebModel();
            if (id != null)
            {
                var brand = brandService.GetBrand((long)id);
                if (brand != null)
                    model = brand.CreateFromServerToClient();
            }
            return View(model);
        }

        //
        // POST: /Brand/Create
        [HttpPost]
        public ActionResult Create(BrandWebModel brand)
        {
            long flag = 0;
            try
            {

                if (brand.BrandId == 0)
                {
                    brand.RecCreatedBy = User.Identity.Name;
                    brand.RecCreatedDate = DateTime.Now;
                    flag = brandService.CheckBrandName(brand.BrandName);
                }
                if (flag != 1)
                {
                    brand.RecLastUpdatedBy = User.Identity.Name;
                    brand.RecLastUpdatedDate = DateTime.Now;
                    if (brandService.AddBrand(brand.CreateFromClientToServer()) > 0)
                    {
                        //Product Saved
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Brand has been saved successfully.",
                            IsSaved = true
                        };
                    }
                }
                else
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = "Brand already exists.",
                        IsSaved = true
                    };
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}