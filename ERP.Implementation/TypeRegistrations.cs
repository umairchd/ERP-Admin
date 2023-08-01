using ERP.Interfaces.IServices;
using ERP.Models.IdentityModels;
using ERP.Service.Identity;
using ERP.Service.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;

namespace ERP.Service
{
    public static class TypeRegistrations
    {
        public static void RegisterType(IUnityContainer unityContainer)
        {
            UnityConfig.UnityContainer = unityContainer;
            Repository.TypeRegistrations.RegisterType(unityContainer);
            unityContainer.RegisterType<ILogger, LoggerService>();
            unityContainer.RegisterType<IAspNetUserService, AspNetUserService>();
            unityContainer.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            unityContainer.RegisterType<IProductCategoryService, ProductCategoryService>();
            unityContainer.RegisterType<IProductService, ProductService>();
            unityContainer.RegisterType<IVendorService, VendorService>();
            unityContainer.RegisterType<IInventoryItemService, InventoryItemService>();
            unityContainer.RegisterType<IOrdersService, OrdersService>();
            unityContainer.RegisterType<IOrderItemService, OrderItemsService>();
            unityContainer.RegisterType<IProductConfigurationService, ProductConfigurationService>();
            unityContainer.RegisterType<ICustomerService, CustomerService>();
            unityContainer.RegisterType<IExpenseCategoryService, ExpenseCategoryService>();
            unityContainer.RegisterType<IExpenseService, ExpenseService>();
            unityContainer.RegisterType<IReportsService, ReportsService>();
            unityContainer.RegisterType<INotesCategoryService, NotesCategoryService>();
            unityContainer.RegisterType<INoteService, NoteService>();
            unityContainer.RegisterType<IProductMainCategoryService, ProductMainCategoryService>();
            unityContainer.RegisterType<IProductImageService, ProductImageService>();
            unityContainer.RegisterType<ITopSellingProductsViewService, TopSellingProductsViewService>();
            unityContainer.RegisterType<IBrandService, BrandService>();
            unityContainer.RegisterType<IUnitService, UnitService>();
            unityContainer.RegisterType<IShelfService, ShelfService>();
            unityContainer.RegisterType<IDashboardService, DashboardService>();
            unityContainer.RegisterType<IWebSiteSliderService, WebSiteSliderService>();
            unityContainer.RegisterType<IPurchaseBillService, PurchaseBillService>();
            unityContainer.RegisterType<IProductVariationService, ProductVariationService>();
            unityContainer.RegisterType<ISupplierBankAccountService, SupplierBankAccountService>();
            unityContainer.RegisterType<ISupplierPaymentHistoryService, SupplierPaymentHistoryService>();
            unityContainer.RegisterType<IReturnedItemService, ReturnedItemService>();
        }
    }
}
