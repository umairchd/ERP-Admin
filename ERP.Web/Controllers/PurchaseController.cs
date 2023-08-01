using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;
using ERP.Models.DropdownListModels;
using ERP.Models.ModelMapers;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;
using ERP.Models.WebModels;
using ERP.Models.WebViewModels;
using PurchaseBill = ERP.Models.DomainModels.PurchaseBill;

namespace ERP.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PurchaseController : BaseController
    {
        private readonly IInventoryItemService inventoryItemService;
        private readonly IPurchaseBillService purchaseBillService;
        private readonly IProductService productService;
        private readonly IVendorService vendorService;
        private readonly IProductConfigurationService configService;

        public PurchaseController(IInventoryItemService inventoryItemService, IPurchaseBillService purchaseBillService, IProductService productService, IVendorService vendorService, IProductConfigurationService configService)
        {
            this.inventoryItemService = inventoryItemService;
            this.purchaseBillService = purchaseBillService;
            this.productService = productService;
            this.vendorService = vendorService;
            this.configService = configService;
        }

        // GET: Inventory
        //public ActionResult History()
        //{
        //    var searchRequest = Session["PageMetaData"] as InventoryItemSearchRequest;
        //    Session["PageMetaData"] = null;
        //    ViewBag.MessageVM = TempData["message"] as MessageViewModel;

        //    var vendors = vendorService.GetAllVendors().ToList();
        //    var viewModel = new InventoryItemsListViewModel
        //    {
        //        SearchRequest = searchRequest ?? new InventoryItemSearchRequest(),
        //        Vendors = vendors.Any()
        //            ? vendors.Select(x => x.CreateFromServerToClient())
        //            : new List<VendorModel>()
        //    };

        //    return View(viewModel);
        //}
        //[HttpPost]
        //public ActionResult History(InventoryItemSearchRequest searchRequest)
        //{
        //    var viewModel = new InventoryItemsListViewModel();
        //    try
        //    {
        //        var searchResponse = inventoryItemService.GetInventoryItemSearchResponse(searchRequest);

        //        var resultData = searchResponse.InventoryItems.Any()
        //            ? searchResponse.InventoryItems.Select(x => x.CreateListServerToClient()).ToList()
        //            : new List<InventoryItemListModel>();

        //        viewModel.data = resultData;
        //        viewModel.recordsTotal = searchResponse.TotalCount;
        //        viewModel.recordsFiltered = searchResponse.FilteredCount;

        //        // Keep Search Request in Session
        //        Session["PageMetaData"] = searchRequest;
        //        return Json(viewModel, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        TempData["message"] = new MessageViewModel { Message = "There is some problem, please try again.", IsError = true };
        //        return View(viewModel);
        //    }
        //}

        // GET: Inventory/Details/5

        /*Added by Jahangir*/
        public ActionResult History()
        {
            //var searchRequest = Session["PageMetaData"] as PuchaseBillsSearchRequest;
            //Session["PageMetaData"] = null;
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;

            var vendors = vendorService.GetAllVendors().ToList();
            var viewModel = new PurchaseBillHistoryViewModel
            {
                data = new List<PurchaseBillHistory>(),
                VendorsDdl = vendors.Any()
                    ? vendors.Select(x => x.MapVendorsDDL()).ToList()
                    : new List<VendorsDdl>()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult History(PuchaseBillsSearchRequest searchRequest)
        {
            var viewModel = new PurchaseBillHistoryViewModel();
            try
            {
                var searchResponse = purchaseBillService.SearchPurchaseBills(searchRequest);

                var resultData = searchResponse.PurchaseBills.Any()
                    ? searchResponse.PurchaseBills.Select(x => x.MapBillHistoryFromServerToClient()).ToList()
                    : new List<PurchaseBillHistory>();
                viewModel.data = resultData;
                viewModel.recordsFiltered = searchResponse.FilteredCount;
                viewModel.recordsTotal = searchResponse.TotalCount;

                // Keep Search Request in Session
                //Session["PageMetaData"] = searchRequest;
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                TempData["message"] = new MessageViewModel { Message = "There is some problem, please try again.", IsError = true };
                return View(viewModel);
            }
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        //[SiteAuthorize(IsModule = true)]
        public ActionResult New(long? id)
        {
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;

            var purchaseBillViewModel = purchaseBillService.GetPurchaseBillResponse(id);

            if (purchaseBillViewModel.PurchaseBill != null)
            {
                var defaultVendorId = "";
                defaultVendorId = TempData["LastSavedVendorId"] != null
                    ? TempData["LastSavedVendorId"].ToString()
                    : new Utility().GetDefaultVendor(Session, configService);
                if (!string.IsNullOrEmpty(defaultVendorId))
                    purchaseBillViewModel.PurchaseBill.VendorId = Convert.ToInt64(defaultVendorId);
                purchaseBillViewModel.PurchaseBill.PurchaseDate = DateTime.Now;
            }
            else
            {
                purchaseBillViewModel.SupplierPaymentHistory.PaymentMethodId = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPaymentMethod"]);
            }
            return View(purchaseBillViewModel);
        }

        // POST: Inventory/Create
        [HttpPost]
        public ActionResult New(PurchaseBillViewModel viewModel)
        {
            try
            {
                if (viewModel.PurchaseBill.PurchaseBillId == 0)
                {
                    viewModel.PurchaseBill.RecCreatedBy = Session["UserID"].ToString();
                    viewModel.PurchaseBill.RecCreatedDate = DateTime.Now;
                }
                viewModel.PurchaseBill.RecLastUpdatedBy = Session["UserID"].ToString();
                viewModel.PurchaseBill.RecLastUpdatedDate = DateTime.Now;


                PurchaseBill purchaseBill = viewModel.PurchaseBill.CreateFromClientToServer();
                purchaseBill.PurchaseBillItems =
                    viewModel.InventoryItems.Select(x => x.CreateFromClientToServer()).ToList();


                if (viewModel.SupplierPaymentHistory.SupplierPaymentHistoryId == 0)
                {
                    viewModel.SupplierPaymentHistory.RecCreatedBy = Session["UserID"].ToString();
                    viewModel.SupplierPaymentHistory.RecCreatedDate = DateTime.Now;
                }
                viewModel.SupplierPaymentHistory.RecLastUpdatedBy = Session["UserID"].ToString();
                viewModel.SupplierPaymentHistory.RecLastUpdatedDate = DateTime.Now;

                //set PaidAmount from Payment Details to PurchaseBill
                purchaseBill.PaidAmount = viewModel.SupplierPaymentHistory.Amount;
                SupplierPaymentHistory supplierPaymentHistory =
                    viewModel.SupplierPaymentHistory.CreateFromClientToServer();

                PurchaseBillWithPayment purchaseBillWithPayment = new PurchaseBillWithPayment
                {
                    PurchaseBill = purchaseBill,
                    SupplierPaymentHistory = supplierPaymentHistory
                };

                if (purchaseBillService.AddPurchaseBillWithPayment(purchaseBillWithPayment))
                {
                    //Saved
                    TempData["message"] = new MessageViewModel { Message = "Purchase bill has been saved successfully.", IsSaved = true };
                }
                if (Request.Form["save"] != null)
                    return RedirectToAction("History");

                TempData["LastSavedVendorId"] = viewModel.PurchaseBill.VendorId;
                return RedirectToAction("New");

                //return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View();
            }
        }

    }
}
