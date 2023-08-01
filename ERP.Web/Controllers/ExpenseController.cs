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
    public class ExpenseController : BaseController
    {
        private readonly IExpenseService expenseService;
        private readonly IExpenseCategoryService expenseCategoryService;
        private readonly IVendorService vendorService;

        public ExpenseController(IExpenseService expenseService, IExpenseCategoryService expenseCategoryService, IVendorService vendorService)
        {
            this.expenseService = expenseService;
            this.expenseCategoryService = expenseCategoryService;
            this.vendorService = vendorService;
        }
        //
        // GET: /Expense/
        public ActionResult Index()
        {
            IEnumerable<ExpenseModel> expenses = expenseService.GetAllExpenses().Select(x => x.CreateFromServerToClient());
            return View(expenses);
        }

        //
        // GET: /Expense/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Expense/Create
        public ActionResult Create(long? id)
        {
            ExpenseViewModel model = new ExpenseViewModel();
            model.ExpenseCategories = expenseCategoryService.GetAllExpenseCategories().Select(x => x.CreateFromServerToClient());
            model.Vendors = vendorService.GetAllVendors().Select(x => x.CreateFromServerToClient());
            if (id != null)
            {
                var expense = expenseService.GetExpense((long)id);
                if (expense != null)
                    model.ExpenseModel = expense.CreateFromServerToClient();
            }
            return View(model);
        }

        //
        // POST: /Expense/Create
        [HttpPost]
        public ActionResult Create(ExpenseViewModel evm)
        {
            try
            {
                if (evm.ExpenseModel.Id == 0)
                {
                    evm.ExpenseModel.RecCreatedBy = User.Identity.Name;
                    evm.ExpenseModel.RecCreatedDate = DateTime.Now;
                }
                evm.ExpenseModel.RecLastUpdatedBy = User.Identity.Name;
                evm.ExpenseModel.RecLastUpdatedDate = DateTime.Now;

                var epxToSave = evm.ExpenseModel.CreateFromClientToServer();
                if (expenseService.AddExpense(epxToSave) > 0)
                {
                    //Product Saved
                    TempData["message"] = new MessageViewModel { Message = "Expense has been saved successfully.", IsSaved = true };
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Expense/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Expense/Edit/5
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
        // GET: /Expense/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Expense/Delete/5
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
