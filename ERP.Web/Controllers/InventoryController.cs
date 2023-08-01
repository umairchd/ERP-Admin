using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;
using ERP.Models.ModelMapers;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;
using ERP.Models.WebModels;
using ERP.Models.WebViewModels;
using Microsoft.AspNet.Identity;
using ProductImage = ERP.Models.WebModels.ProductImage;

namespace ERP.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InventoryController : BaseController
    {
        private readonly IProductService productService;
        private readonly IProductCategoryService productCategoryService;
        private readonly IProductImageService productImageService;

        public InventoryController(IProductService productService,IProductCategoryService productCategoryService,IProductImageService productImageService)
        {
            this.productService = productService;
            this.productCategoryService = productCategoryService;
            this.productImageService = productImageService;
        }

        // GET: Products
        public ActionResult All()
        {
            ProductSearchRequest searchRequest = Session["PageMetaData"] as ProductSearchRequest;
            Session["PageMetaData"] = null;
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;

            var categories = productCategoryService.GetAllSubCategories().ToList();
            return View(new ProductsListViewModel
            {
                SearchRequest = searchRequest ?? new ProductSearchRequest(),
                ProductCategories = categories.Any()?categories.Select(x=>x.CreateForDDL()):new List<ProductCategoryDDLModel>()
            });
        }
        [HttpPost]
        public ActionResult All(ProductSearchRequest searchRequest)
        {
            var viewModel = new ProductsListViewModel();
            try
            {
                var searchResponse = productService.GetProductSearchResponse(searchRequest);

                var resultData = searchResponse.Products.Any()
                    ? searchResponse.Products.Select(x => x.CreateFromServerToClientList()).ToList()
                    : new List<ProductModel>();

                viewModel.data = resultData;
                viewModel.recordsTotal = searchResponse.TotalCount;
                viewModel.recordsFiltered = searchResponse.FilteredCount;

                // Keep Search Request in Session
                Session["PageMetaData"] = searchRequest;
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                TempData["message"] = new MessageViewModel { Message = "There is some problem, please try again.", IsError = true };
                return View(viewModel);
            }
        }
        ////Product Variations
        //public ActionResult All(ProductSearchRequest searchRequest)
        //{
        //    var viewModel = new ProductsListViewModel();
        //    try
        //    {
        //        ProductVariationSearchResponse searchResponse = productService.GetProductVariationSearchResponse(searchRequest);

        //        var resultData = searchResponse.ProductVariations.Any()
        //            ? searchResponse.ProductVariations.Select(x => x.CreateFromServerToClientList()).ToList()
        //            : new List<ProductVariationModel>();

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

        // GET: Product/New
        public ActionResult New(long? id)
        {
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            ProductViewModel productViewModel=new ProductViewModel();
            ProductResponse responseResult = productService.GetProductResponse(id);
            if (responseResult.ProductCategories.Any())
            {
                productViewModel.ProductCategories = responseResult.ProductCategories.Select(x => x.CreateForDDL());
                productViewModel.ProductBrands = responseResult.ProductBrands.Select(x => x.CreateFromServerToClient());
                productViewModel.Units = responseResult.Units.Select(x => x.CreateFromServerToClient());
                productViewModel.Shelves = responseResult.Shelves.Select(x => x.CreateFromServerToClient());
            }
            if (responseResult.Product != null)
            {
                productViewModel.ProductModel = responseResult.Product.CreateFromServerToClient();
                productViewModel.ProductVariations = responseResult.Product.ProductVariations.Select(x=>x.CreateFromServerToClient()).ToList();
            }
                
            var lastSavedCategoryID = TempData["LastCategoryId"];
            var lastSavedBrandID = TempData["LastBrandId"];
            if (lastSavedCategoryID != null)
            {
                if (productViewModel.ProductModel== null)
                    productViewModel.ProductModel = new ProductModel();
                productViewModel.ProductModel.CategoryId = long.Parse(lastSavedCategoryID.ToString());
                productViewModel.ProductModel.BrandId = long.Parse(lastSavedBrandID.ToString());
            }

            ViewBag.LastSavedId = TempData["LastSavedId"];
            return View(productViewModel);
        }

        // POST: Inventory/New
        [HttpPost]
        public ActionResult New(ProductViewModel productViewModel)
        {
            try
            {
                bool isCreated = false;
                if (productViewModel.ProductModel.ProductId == 0)
                {
                    productViewModel.ProductModel.RecCreatedBy = User.Identity.Name;
                    productViewModel.ProductModel.RecCreatedDate = DateTime.Now;
                    isCreated = true;
                }
                productViewModel.ProductModel.RecLastUpdatedBy = User.Identity.Name;
                productViewModel.ProductModel.RecLastUpdatedDate = DateTime.Now;

                //Minimum sale price should not be less than purchase price
                //if (productViewModel.ProductModel.MinSalePriceAllowed <
                //    productViewModel.ProductModel.PurchasePrice)
                //    productViewModel.ProductModel.MinSalePriceAllowed =
                //        productViewModel.ProductModel.SalePrice;
                var productToBeSaved = productViewModel.ProductModel.CreateFromClientToServer();

                productToBeSaved.ProductVariations =
                    productViewModel.ProductVariations.Select(x => x.SetProductVariationCreatorUpdatorAndCreateFromClientToServer(User.Identity.GetUserId())).ToList();
                var lastSavedId = productService.SaveProduct(productToBeSaved);


                //Save image to Db
                if (productViewModel.ProductDefaultImage != null)
                {
                    //SaveProductImage(productViewModel, lastSavedId);
                }
                
                if (lastSavedId > 0)
                {
                    if (isCreated)
                    {
                        //Product Saved
                        TempData["message"] = new MessageViewModel { Message = "Product has been saved successfully.<br/>Last saved product id is " + lastSavedId, IsSaved = true };
                    }
                    else
                    {
                        //Product Updated
                        TempData["message"] = new MessageViewModel { Message = "Product has been updated successfully.<br/>Updated product id is " + lastSavedId, IsUpdated = true };
                    }
                }

                if (Request.Form["save"]!=null)
                    return RedirectToAction("All");
                if (isCreated)
                {
                    TempData["LastSavedId"] = lastSavedId;
                    
                    TempData["LastCategoryId"] = productViewModel.ProductModel.CategoryId;

                    TempData["LastBrandId"] = productViewModel.ProductModel.BrandId;
                }
                return RedirectToAction("New");
            }
            catch(Exception exception)
            {
                return View();
            }
        }

        public ActionResult PrintBarcode()
        {
            IEnumerable<ProductCategory> productCategories = productCategoryService.GetAllProductCategories();
            return View(productCategories.Select(x => x.CreateFromServerToClient()));

        }

        public JsonResult GetProductsByCategoryId(long id)
        {
            IEnumerable<ProductVariation> product = productService.GetProductVariationsByCategories(0, id);
            return Json(product.Select(x => x.MapProductVariationServerToClient()), JsonRequestBehavior.AllowGet);
        }
        private void SaveProductImage(ProductViewModel productViewModel, long lastSavedId)
        {
            var tempStream = productViewModel.ProductDefaultImage.InputStream;

            //File size must be less than 2MBs
            if (productViewModel.ProductDefaultImage.ContentLength > 0 &&
                productViewModel.ProductDefaultImage.ContentLength < 2000000)
            {
                //reisze the image for facebook optimization
                var resizedImage = Utility.ResizeImage(Image.FromStream(tempStream),
                    Utility.GetImageFormat(productViewModel.ProductDefaultImage.ContentType),
                    Convert.ToInt32(ConfigurationManager.AppSettings["ProductImageWidth"]),
                    Convert.ToInt32(ConfigurationManager.AppSettings["ProductImageHeight"]), true);

                byte[] bytes = new byte[resizedImage.Length];
                //copy file content to array
                resizedImage.Read(bytes, 0, Convert.ToInt32(resizedImage.Length));

                ProductImage productImage = new ProductImage
                {
                    ProductVariationId = lastSavedId,
                    ImageData = bytes,
                    ContentType = productViewModel.ProductDefaultImage.ContentType,
                    IsDefault = true,
                    RecCreatedBy = User.Identity.GetUserId(),
                    RecCreatedDate = DateTime.Now.Date,
                    RecLastUpdatedBy = User.Identity.GetUserId(),
                    RecLastUpdatedDate = DateTime.Now.Date,
                };
                productImageService.AddImage(productImage.MapProductImageClientToServer());
            }
        }
        public ActionResult LoadProductImage(long id)
        {
            //pass id to service, and load image data
            var image = productImageService.ProductImage(id);

            if (image != null && image.ImageData != null)
            {
                string ext = image.ContentType.Split('/')[1];
                return File(image.ImageData, image.ContentType, "IMG_" + image.ImageId + ((DateTime)image.RecLastUpdatedDate).ToString("yyyyMMdd_HHmmss") + "." + ext);
            }
            return File(new byte[] { }, "image/png", "null.png");
        }

        public JsonResult DeleteProduct(long id)
        {
            var response = productService.DeleteProductVariation(id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
