using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ERP.Interfaces.IServices;
using ERP.Models.DropdownListModels;
using ERP.Models.ModelMapers;
using ERP.Models.RequestModels;
using ERP.Models.WebModels;
using ERP.Models.WebViewModels;

namespace ERP.Web.Controllers
{
    [Authorize(Roles = "Admin , Employee")]
    public class SupplierPaymentHistoryController : BaseController
    {
        private readonly ISupplierPaymentHistoryService paymentHistoryService;
        private readonly IVendorService vendorService;

        public SupplierPaymentHistoryController(ISupplierPaymentHistoryService paymentHistoryService, IVendorService vendorService)
        {
            this.paymentHistoryService = paymentHistoryService;
            this.vendorService = vendorService;
        }

        public ActionResult History()
        {
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;

            var vendors = vendorService.GetAllVendors().ToList();
            var viewModel = new SupplierPaymentHistoryViewModel
            {
                data = new List<SupplierPaymentHistoryWebModel>(),
                VendorsDdl = vendors.Any()
                    ? vendors.Select(x => x.MapVendorsDDL()).ToList()
                    : new List<VendorsDdl>()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult History(SupplierPaymentHistorySearchRequest searchRequest)
        {
            var viewModel = new SupplierPaymentHistoryViewModel();
            try
            {
                var searchResponse = paymentHistoryService.GetSupplierPaymentHistorySearchResponse(searchRequest);

                var resultData = searchResponse.PaymentHistoryList.Any()
                    ? searchResponse.PaymentHistoryList.Select(x => x.CreateHistoryFromServerToClient()).ToList()
                    : new List<SupplierPaymentHistoryWebModel>();

                viewModel.data = resultData;
                viewModel.recordsFiltered = searchResponse.FilteredCount;
                viewModel.recordsTotal = searchResponse.TotalCount;
                viewModel.BalanceAmount = searchResponse.BalanceAmount;
                viewModel.CreditAmount = searchResponse.CreditAmount;
                viewModel.PaidAmount = searchResponse.PaidAmount;

                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                TempData["message"] = new MessageViewModel { Message = "There is some problem, please try again.", IsError = true };
                return View(viewModel);
            }
        }

        public ActionResult New(long? id)
        {
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;

            var paymentBaseData = paymentHistoryService.GetPaymentHistoryBaseData(id);

            var model = new SupplierPaymentHistoryCreateViewModel
            {
                PaymentMethods = paymentBaseData.PaymentMethods.Select(x => x.CreateFromServerToClient()),
                VendorsDdl = paymentBaseData.Vendors.Select(x => x.MapVendorsDDL()),
                SupplierBankAccounts = paymentBaseData.SupplierBankAccounts.Select(x => x.CreateFromServerToClient())
            };
            if (paymentBaseData.SupplierPaymentHistory != null)
                model.PaymentHistoryWebModel = paymentBaseData.SupplierPaymentHistory.CreateFromServerToClient();



            return View(model);
        }

        [HttpPost]
        public ActionResult New(SupplierPaymentHistoryCreateViewModel paymentHistory)
        {
            if (paymentHistory.PaymentHistoryWebModel.SupplierPaymentHistoryId == 0)
            {
                paymentHistory.PaymentHistoryWebModel.RecCreatedDate = DateTime.Now;
                paymentHistory.PaymentHistoryWebModel.RecCreatedBy = Session["UserID"].ToString();
            }
            paymentHistory.PaymentHistoryWebModel.RecLastUpdatedBy = Session["UserID"].ToString();
            paymentHistory.PaymentHistoryWebModel.RecLastUpdatedDate = DateTime.Now;

            if (paymentHistoryService.SaveOrUpdate(paymentHistory.PaymentHistoryWebModel.CreateFromClientToServer()))
            {
                TempData["message"] = new MessageViewModel
                    {
                        IsSaved = true,
                        Message = "Payment has been saved successfully."
                    };
            }
            else
            {
                TempData["message"] = new MessageViewModel
                {
                    IsError = true,
                    Message = "Payment could not be saved. Try again"
                };
            }

            if (Request.Form["saveNew"] == null)
                return RedirectToAction("History");

            return RedirectToAction("New", new { id = 0 });
        }
    }
}