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
    public class ExpenseCategoryController : BaseController
    {
        private readonly IExpenseCategoryService expenseCategoryService;

        public ExpenseCategoryController(IExpenseCategoryService expenseCategoryService)
        {
            this.expenseCategoryService = expenseCategoryService;
        }
        //
        // GET: /ExpenseCategory/
        public ActionResult Index()
        {
            IEnumerable<ExpenseCategoryModel> expenseCategories = expenseCategoryService.GetAllExpenseCategories().Select(x => x.CreateFromServerToClient());
            return View(expenseCategories);
        }

        //
        // GET: /ExpenseCategory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ExpenseCategory/Create
        public ActionResult Create(long? id)
        {
            ExpenseCategoryModel model = new ExpenseCategoryModel();
            if (id != null)
            {
                var category = expenseCategoryService.GetExpenseCategory((long)id);
                if (category != null)
                    model = category.CreateFromServerToClient();
            }
            return View(model);
        }

        //
        // POST: /ExpenseCategory/Create
        [HttpPost]
        public ActionResult Create(ExpenseCategoryModel expenseCategory)
        {
            try
            {
                if (expenseCategory.Id == 0)
                {
                    expenseCategory.RecCreatedBy = User.Identity.Name;
                    expenseCategory.RecCreatedDate = DateTime.Now;
                }
                expenseCategory.RecLastUpdatedBy = User.Identity.Name;
                expenseCategory.RecLastUpdatedDate = DateTime.Now;


                if (expenseCategoryService.AddExpenseCategory(expenseCategory.CreateFromClientToServer()) > 0)
                {
                    //Product Saved
                    TempData["message"] = new MessageViewModel { Message = "Expense category has been saved successfully.", IsSaved = true };
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ExpenseCategory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ExpenseCategory/Edit/5
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
        // GET: /ExpenseCategory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ExpenseCategory/Delete/5
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
