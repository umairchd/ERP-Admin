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
    public class ProductCategoryController : BaseController
    {
        private readonly IProductCategoryService productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            this.productCategoryService = productCategoryService;
        }
        // GET: ProductCategory
        public ActionResult Index()
        {
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            IEnumerable<ProductCategoryModel> categories = productCategoryService.GetAllProductCategories().Select(x=>x.CreateFromServerToClient());
            return View(categories);
        }

        // GET: ProductCategory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductCategory/Create
        public ActionResult Create(long? id)
        {
            var model = new ProductCategoryViewModel();
            var categoryReponse = productCategoryService.GetProductCategoryResponse(id);
            if (categoryReponse.ProductCategory != null)
            {
                model.ProductCategoryModel = categoryReponse.ProductCategory.CreateFromServerToClient();
            }
            model.ProductMainCategoriesDDL = categoryReponse.ProductMainCategories.Select(x=>x.CreateForDDL());
            return View(model);
        }

        // POST: ProductCategory/Create
        [HttpPost]
        public ActionResult Create(ProductCategoryViewModel viewModel)
        {
            try
            {
                if (viewModel.ProductCategoryModel.CategoryId == 0)
                {
                    viewModel.ProductCategoryModel.RecCreatedBy = User.Identity.Name;
                    viewModel.ProductCategoryModel.RecCreatedDate = DateTime.Now;
                }
                viewModel.ProductCategoryModel.RecLastUpdatedBy = User.Identity.Name;
                viewModel.ProductCategoryModel.RecLastUpdatedDate = DateTime.Now;


                if (productCategoryService.AddProductCategory(viewModel.ProductCategoryModel.CreateFromClientToServer()) > 0)
                {
                    //Product Saved
                    TempData["message"] = new MessageViewModel { Message = "Product Category has been saved successfully.", IsSaved = true };
                }
                if (Request.Form["save"] != null)
                    return RedirectToAction("Index");

                return RedirectToAction("Create");
            }
            catch(Exception exception)
            {
                ViewBag.MessageVM = new MessageViewModel
                {
                    IsError = true,
                    Message = exception.Message
                };
                return View(viewModel);
            }
        }

        // GET: ProductCategory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductCategory/Edit/5
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

        // GET: ProductCategory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductCategory/Delete/5
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
