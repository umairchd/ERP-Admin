using System;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;
using ERP.Models.ModelMapers;
using ERP.Models.WebViewModels;
using ERP.WebBase.EncryptDecrypt;

namespace ERP.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConfigurationController : BaseController
    {
        private readonly IProductConfigurationService configurationService;
        private readonly IVendorService vendorService;

        public ConfigurationController(IProductConfigurationService configurationService, IVendorService vendorService)
        {
            this.configurationService = configurationService;
            this.vendorService = vendorService;
        }

        // GET: Configuration
        public ActionResult Index()
        {
            return View();
        }

        // GET: Configuration/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Configuration/Create
        public ActionResult Create()
        {
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            var congif = configurationService.GetDefaultConfiguration();
            var vendor = vendorService.GetActiveVendors();

            if(congif == null)
                congif = new ProductConfiguration();

            ProductConfigurationViewModel oVM = new ProductConfigurationViewModel();
            oVM.Configuration = congif;
            oVM.vendorModel = vendor.Select(x => x.CreateFromServerToClient());
            oVM.LicenseKey = ConfigurationManager.AppSettings["LicenseKey"];

            return View(oVM);
        }

        // POST: Configuration/Create
        [HttpPost]
        public ActionResult Create(ProductConfigurationViewModel configurationViewModel)
        {
            try
            {
                if (configurationViewModel.Configuration.Id == 0)
                {
                    configurationViewModel.Configuration.RecCreatedBy = User.Identity.Name;
                    configurationViewModel.Configuration.RecCreatedDate = DateTime.Now;
                }
                configurationViewModel.Configuration.RecLastUpdatedBy = User.Identity.Name;
                configurationViewModel.Configuration.RecLastUpdatedDate = DateTime.Now;
                if (configurationService.SaveConfiguration(configurationViewModel.Configuration) > 0)
                {
                    Session[Utility.ProductConfiguration] = null;
                    TempData["message"] = new MessageViewModel { Message = "Configuration has been saved successfully.", IsSaved = true };
                }

                //Helps to open the Root level web.config file.
                var webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                //Modifying the AppKey from AppValue to AppValue1
                webConfigApp.AppSettings.Settings["LK"].Value = configurationViewModel.LicenseKey;
                webConfigApp.AppSettings.Settings["LTS"].Value = StringCipher.Encrypt(DateTime.Now.ToString("G"), "4006");
                //Save the Modified settings of AppSettings.
                webConfigApp.Save();


                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: Configuration/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Configuration/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Configuration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Configuration/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
