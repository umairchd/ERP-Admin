﻿using System;
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
    public class VendorController : BaseController
    {
        private readonly IVendorService vendorService;

        public VendorController(IVendorService vendorService)
        {
            this.vendorService = vendorService;
        }
        //
        // GET: /Vendor/
        public ActionResult Index()
        {
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            IEnumerable<VendorModel> vendors = vendorService.GetAllVendors().Select(x => x.CreateFromServerToClient());
            return View(vendors);
        }

        //
        // GET: /Vendor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Vendor/Create
        public ActionResult Create(long? id)
        {
            VendorModel model = new VendorModel();            
            if (id != null)
            {
                var vendor = vendorService.GetVendor((long)id);
                if (vendor != null)
                    model = vendor.CreateFromServerToClient();
            }
            return View(model);
        }

        //
        // POST: /Vendor/Create
        [HttpPost]
        public ActionResult Create(VendorModel vendor)
        {
            try
            {
                vendor.ActiveFlag = true;
                if (vendor.VendorId == 0)
                {
                    vendor.RecCreatedBy = User.Identity.Name;
                    vendor.RecCreatedDate = DateTime.Now;
                }
                vendor.RecLastUpdatedBy = User.Identity.Name;
                vendor.RecLastUpdatedDate = DateTime.Now;
                if (vendorService.AddVendor(vendor.CreateFromClientToServer()) > 0)
                {
                    //Product Saved
                    TempData["message"] = new MessageViewModel { Message = "Vendor has been saved successfully.", IsSaved = true };
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Vendor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Vendor/Edit/5
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

        //
        // GET: /Vendor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Vendor/Delete/5
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
