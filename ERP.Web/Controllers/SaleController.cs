using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;
using ERP.Models.ModelMapers;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;
using ERP.Models.WebModels;
using ERP.Models.WebViewModels;

namespace ERP.Web.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class SaleController : BaseController
    {
        private readonly ICustomerService customerService;
        private readonly IOrdersService orderService;
        private readonly IProductService productService;
        private readonly IOrderItemService orderItemService;
        private readonly IProductConfigurationService configurationService;
        private readonly IReturnedItemService returnedItemsService;


        public ActionResult History()
        {
            OrderSearchRequest viewModel = Session["PageMetaData"] as OrderSearchRequest;

            Session["PageMetaData"] = null;
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            var toReturnModel = new OrderViewModel
            {
                SearchRequest = viewModel ?? new OrderSearchRequest()
            };
            
            return View(toReturnModel);
        }
        [HttpPost]
        public ActionResult History(OrderSearchRequest oRequest)
        {
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            OrderSearchResponse oResponse = orderService.GetOrdersSearchResponse(oRequest);
            List<OrderListViewModel> oList = oResponse.Orders.Select(x => x.CreateFromServerToLVClient()).ToList();
            var oVModel = new OrderViewModel
            {
                data = oList,
                recordsTotal = oResponse.TotalCount,
                recordsFiltered = oResponse.FilteredCount,
                GrossSale = oList.Sum(x => x.SubTotal),
                Discount = oList.Sum(x => x.TotalDiscount),
                NetSale = oList.Sum(x => x.NetTotal)
            };


            Session["PageMetaData"] = oRequest;
            var toReturn = Json(oVModel, JsonRequestBehavior.AllowGet);
            return toReturn;
        }

        public SaleController(ICustomerService customerService,IOrdersService orderService, IProductService productService, IOrderItemService orderItemService, IProductConfigurationService configurationService, IReturnedItemService returnedItemsService)
        {
            this.customerService = customerService;
            this.orderService = orderService;
            this.productService = productService;
            this.orderItemService = orderItemService;
            this.configurationService = configurationService;
            this.returnedItemsService = returnedItemsService;
        }

        // GET: ProductCategory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductCategory/Create
        public ActionResult New(long ? id)
        {
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            var products = productService.GetAllProductVariations().ToList().Select(x => x.MapProductServerToClientForSaleInvoice()).OrderBy(x=>x.Name).ToList();
            ViewBag.Products = products;
            var customers = customerService.GetAllCustomers().ToList().Select(x=>x.CreateFromServerToClient()).ToList();
            ViewBag.Customers = customers;
            OrderModel toSend = new OrderModel();
            if (id == null || id == 0)
                toSend.OrderItems = new List<OrderItemModelWithProduct>();
            else
            {
                //Means Edit case
                toSend = orderService.GetOrders(id.Value).CreateFromServerToClient();
            }
            //Setting Max discount
            int MaxDiscountInt = 0;
            int.TryParse( new Utility().GetConfigMaxDiscount(Session, configurationService,User.IsInRole(Utility.AdminRoleName)), out MaxDiscountInt);
            toSend.AllowedMaxDiscount = MaxDiscountInt;

            
            return View(toSend);
        }

        // POST: ProductCategory/Create
        [HttpPost]
        public ActionResult New(OrderModel orderDetail)
        {
            try
            {
                SetOrderItems(orderDetail);
                //string email = new Utility().GetConfigEmail(Session, configurationService);
              
                if (orderDetail.OrderId <= 0)
                {
                    //write function to fill up the list customers list in orders
                    var customers = new List<CustomerOrder>();
                    if (orderDetail.CustomerIds!=null)
                    {
                        customers.AddRange(orderDetail.CustomerIds.Select(customerId => new CustomerOrder
                        {
                            CustomerId = Convert.ToInt64(customerId)
                        }));
                    }
                    
                    var order = orderDetail.CreateFromClientToServer();
                    if (ValidateDiscount(order))
                    {
                        order.CustomerOrders = customers;
                        orderService.AddService(order);
                        orderDetail.OrderId = order.OrderId;
                        //new Task(() => { SendEmail(order, email); }).Start();
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Order has been created with ID: " + order.OrderId,
                            IsSaved = true
                        };
                        if (Request.Form["save"] != null)
                            return RedirectToAction("New");
                        return RedirectToAction("Print", "Sale", new { id = order.OrderId });
                    }
                    ViewBag.MessageVM = new MessageViewModel
                    {
                        Message = "Order Discount Exceed the permit limit",
                        IsError = true
                    };
                    return View(orderDetail);
                }
                else
                {
                    var order = orderDetail.CreateFromClientToServer();
                    
                    orderService.UpdateService(order);
                    orderItemService.AddUpdateService(order);
                    //new Task(() => { SendEmail(order, email); }).Start();
                    TempData["message"] = new MessageViewModel { Message = "Order has been updated successfully.", IsSaved = true };
                    return RedirectToAction("History");
                }
                
            //    new Task(() => { Foo2(42, "life, the universe, and everything"); }).Start();
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        //function to save order against customers
        private bool ValidateDiscount(Order order)
        {
            var maxDiscAllowed = decimal.Parse( new Utility().GetConfigMaxDiscount(Session, configurationService,User.IsInRole(Utility.AdminRoleName)))/100;
            var sumDiscount = order.OrderItems.Sum(x => x.Discount);
            var sumRs = order.OrderItems.Sum(x => x.SalePrice*x.Quantity);
            decimal perc = sumDiscount / sumRs;

            if (perc > maxDiscAllowed)
                return false;
            return true;
        }

        //private bool SendEmail(Order order,string email)
        //{
        //    if (string.IsNullOrEmpty(email) || email.ToLower() == "none")
        //    {
        //        return false;
        //    }
        //    string subject = "";
        //    if (order.IsModified)
        //    {
        //        subject = "Modified: Order: " + order.OrderId;
        //    }
        //    else
        //    {
        //        subject = "Created: Order: " + order.OrderId;
        //    }
        //    var grossSale = order.OrderItems.Sum(x => x.SalePrice*x.Quantity);
        //    var totalDiscount = order.OrderItems.Sum(x => x.Discount);
        //    string body = "Gross Sale: " + grossSale;
        //    body += " Total Discount: " + totalDiscount;
        //    body += " Total Qty: " + order.OrderItems.Sum(x => x.Quantity);
        //    body += " Net Sale: " + (grossSale - totalDiscount).ToString();
        //    string szProdcutCode = string.Empty;
        //    string szProductName = string.Empty;

        //    foreach (var item in order.OrderItems)
        //    {
        //        szProdcutCode += item.ProductId+",";
        //        if(item.Product != null)
        //            szProductName += item.Product.Name+",";
        //    }
        //    body += " CODEs " + szProdcutCode + " Names " + szProductName;
        //    if (order.IsModified)
        //    {
        //        //Just enter that order was modified
        //        body = "Order Modified at: " + DateTime.Now.ToShortDateString() + " By:" + User.Identity.Name;
        //    }
        //    Utility.SendEmailAsync(email,subject,body);
        //    return true;
        //    //Utility.SendEmailAsync(email,"");
        //}
        private void SetOrderItems(OrderModel orderDetail)
        {
            string name = Session["UserID"].ToString();
            if (orderDetail.OrderId <= 0)
            {
                orderDetail.RecCreatedDate = orderDetail.RecLastUpdatedDate = DateTime.Now;

                orderDetail.RecCreatedBy = orderDetail.RecLastUpdatedBy = name;
            }
            else
            {
                orderDetail.RecLastUpdatedDate = DateTime.Now;
                orderDetail.RecLastUpdatedBy = User.Identity.Name;

            }
            List<OrderItemModelWithProduct> NotUpdatedList = new List<OrderItemModelWithProduct>();

            foreach (var item in orderDetail.OrderItems)
            {
                if (item.OrderItemId <= 0)
                {
                    item.RecCreatedDate = item.RecLastUpdatedDate = DateTime.Now;
                    item.RecCreatedBy = item.RecLastUpdatedBy = User.Identity.Name;
                    //GetSalePrice and set it
                    var product = productService.GetProductByAnyCode(item.ProductId.ToString());//ProductId actually is ProductVariationId
                    item.MinSalePriceAllowed = product.Product.Saleprice;
                    item.PurchasePrice = product.Product.Purchaseprice;
                    item.SalePrice = product.Product.Saleprice;
                    if(orderDetail.OrderId>0)
                        orderDetail.IsModified = true;//Means a previous order had a new entery. I know this because orderid >0 and orderitem id<=0
                }
                else
                {
                    if (item.IsModified)
                    {
                        item.RecLastUpdatedBy = name;
                        item.RecLastUpdatedDate = DateTime.Now;
                        //FETCH FROM DB AND SET THE VALUES
                        var orderItem = orderItemService.GetOrderItemById(item.OrderItemId);
                        item.SalePrice = orderItem.SalePrice;
                        item.PurchasePrice = orderItem.PurchasePrice;
                        //item.MinSalePriceAllowed = orderItem.MinSalePriceAllowed;
                        //item.RecCreatedBy = orderItem.RecCreatedBy;
                        //item.RecCreatedDate = orderItem.RecCreatedDate;
                        item.OrderId = orderDetail.OrderId;
                        orderDetail.IsModified = true;//means there is some modification in order. Only qty and Discount can be updated
                    }
                    else
                    {
                        NotUpdatedList .Add(item);
                    }
                }
            }
            foreach (var orderItemModel in NotUpdatedList)
            {
                orderDetail.OrderItems.Remove(orderItemModel);
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

        // POST: ProductCategory/Delete/5
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                if (orderService.DeleteOrder(id))
                {
                    return Json(new { response = "Successfully deleted!", status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { response = "Probably, record has already been deleted.", status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { response = "Oops, there is some problem, please try again.", status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
        }

        //public string GetConfigEmail()
        //{
        //    if (Session[Utility.ConfigEmail] == null)
        //    {
        //        var config = configurationService.GetDefaultConfiguration();
        //        var email = config.Emails;
        //        if (string.IsNullOrEmpty(email))
        //            Session[Utility.ConfigEmail] = "NONE";
        //        else
        //            Session[Utility.ConfigEmail] = email;
        //        return email;
                
        //    }
        //    else
        //    {

        //        return Session[Utility.ConfigEmail].ToString();
        //    }
        //}

        //public string GetConfigMaxDiscount()
        //{
        //    if (User.IsInRole(Utility.AdminRoleName))
        //    {
        //        Session[Utility.MaxDiscount] = 50;
                
        //    }
        //    else if (Session[Utility.MaxDiscount] == null)
        //    {
        //        var config = configurationService.GetDefaultConfiguration();
        //        var MaxAllowedDiscount = config.MaxAllowedDiscount;

        //        Session[Utility.MaxDiscount] = MaxAllowedDiscount;
               

        //    }
        //    return Session[Utility.MaxDiscount].ToString();
        //}
        public ActionResult Print(long id)
        {
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            var oVM = new OrderListViewModel();
            var orderDetails = orderService.GetOrders(id);
            oVM = orderDetails.CreateFromServerToLVClient();
            return View(oVM);
        }
        public ActionResult Return(long? id)
        {
            try
            {
                var returnedOrderItemModel=new ReturnedOrderItemModel();
                if (id != null)
                {
                    //get data
                    returnedOrderItemModel = orderService.GetReturnedOrderItem((long)id);
                }

                ViewBag.MessageVM = TempData["message"] as MessageViewModel;
                return View(returnedOrderItemModel);
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Return(ReturnedOrderItemModel viewModel)
        {
            try
            {
                if (viewModel.ReturnId == 0)
                {
                    viewModel.ReceivedBy = Session["UserID"].ToString();
                    viewModel.ReceivedDate = DateTime.Now;
                }
                
                if (orderService.AddReturnItem(viewModel.CreateFromClientToServer())>0)
                {
                    //Saved
                    TempData["message"] = new MessageViewModel { Message = "Invoice item has been returned successfully.", IsSaved = true };
                }
                if (Request.Form["save"] != null)
                    return RedirectToAction("ReturnHistory");

                return RedirectToAction("Return");
            }
            catch (Exception e)
            {
                TempData["message"] = new MessageViewModel { Message = "Something went wrong.", IsError = true };
                return View();
            }
        }
        public ActionResult ReturnHistory()
        {
            var viewModel = new SaleReturnViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult ReturnHistory(ReturnItemSearchRequest searchRequest)
        {
            var returnItemResponse = orderService.GetReturnItemsSearchResponse(searchRequest);
            var viewModel = new SaleReturnViewModel
            {
                data = returnItemResponse.ReturnedOrderItems.Select(x => x.CreateLVFromServerToClient()).ToList(),
                recordsTotal = returnItemResponse.TotalCount,
                recordsFiltered = returnItemResponse.FilteredCount
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

    }
}
