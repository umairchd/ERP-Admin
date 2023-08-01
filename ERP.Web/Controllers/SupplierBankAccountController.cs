using System;
using System.Linq;
using System.Web.Mvc;
using ERP.Interfaces.IServices;
using ERP.Models.ModelMapers;
using ERP.Models.WebViewModels;

namespace ERP.Web.Controllers
{ 
    [Authorize (Roles = "Admin")]
    public class SupplierBankAccountController : BaseController
    {
        private readonly IVendorService vendorService;
        private readonly ISupplierBankAccountService supplierBankAccountService;


        public SupplierBankAccountController(IVendorService vendorService,ISupplierBankAccountService supplierBankAccountService)
        {
            this.vendorService = vendorService;
            this.supplierBankAccountService = supplierBankAccountService;
        }

        public ActionResult Index()
        {

           var allAccounts=supplierBankAccountService.GetAllSuppliersBankAccounts().Select(x=>x.CreateListFromServerToClient()).ToList();
           return View(allAccounts);
        }
        public ActionResult Create(long? id)
        {
            var supplierAccount = supplierBankAccountService.GetSupplierBankAccount(id);
            //var model = new SupplierBankAccountViewModel();
            //model.Vendors = vendorService.GetAllVendors().Select(x => x.MapVendorsDDL()).ToList();
            return View(supplierAccount);
        }

        [HttpPost]
        public ActionResult Create(SupplierBankAccountViewModel supplierBankViewModel)
        {
            try
            {
                if (supplierBankViewModel.supplierBankAccount.SupplierBankAccountId == 0)
                {
                    supplierBankViewModel.supplierBankAccount.RecCreatedBy = User.Identity.Name;
                    supplierBankViewModel.supplierBankAccount.RecCreatedDate = DateTime.Now;
                }
                supplierBankViewModel.supplierBankAccount.RecLastUpdatedBy = User.Identity.Name;
                supplierBankViewModel.supplierBankAccount.RecLastUpdatedDate = DateTime.Now;

                var accountToSave = supplierBankViewModel.supplierBankAccount.CreateFromClientToServer();
                if (supplierBankAccountService.AddSuppliersBankAccount(accountToSave) == true)
                {
                    TempData["message"] = new MessageViewModel { Message = "Bank Account has been saved successfully.", IsSaved = true };

                }
                if (Request.Form["save"] != null)
                    return RedirectToAction("Index");
                return RedirectToAction("Create",new {id=0});
            }
            catch(Exception e)
            {
                return View();
            }
        }
     
    }
}