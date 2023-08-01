using System.Data.Entity;
using ERP.Interfaces.IRepository;
using ERP.Repository.BaseRepository;
using ERP.Repository.Repositories;
using Microsoft.Practices.Unity;

namespace ERP.Repository
{
    public static class TypeRegistrations
    {
        public static void RegisterType(IUnityContainer unityContainer)
        {
            
            unityContainer.RegisterType<IAspNetUserRepository, AspNetUserRepository>();
            unityContainer.RegisterType<DbContext, BaseDbContext>(new PerRequestLifetimeManager());
            unityContainer.RegisterType<IProductCategoryRepository, ProductCategoryRepository>();
            unityContainer.RegisterType<IProductRepository, ProductRepository>();
            unityContainer.RegisterType<IVendorRepository, SupplierRepository>();
            unityContainer.RegisterType<IPurchaseBillItemRepositoy, PurchaseBillItemRepository>();
            unityContainer.RegisterType<IOrderItemsRepository, OrderItemsRepository>();
            unityContainer.RegisterType<IOrdersRepository, OrdersRepository>();
            unityContainer.RegisterType<IProductConfigurationRepositoy, ProductConfigurationRepository>();
            unityContainer.RegisterType<ICustomerRepository, CustomerRepository>();
            unityContainer.RegisterType<IExpenseCategoryRepository, ExpenseCategoryRepository>();
            unityContainer.RegisterType<IExpenseRepository, ExpenseRepository>();
            unityContainer.RegisterType<IProductMainCategoryRepository, ProductMainCategoryRepository>();
            unityContainer.RegisterType<INotesCategoryRepository, NotesCategoryRepository>();
            unityContainer.RegisterType<INoteRepository, NoteRepository>();
            unityContainer.RegisterType<IProductsStockRepository, ProductsStockRepository>();
            unityContainer.RegisterType<IProductImageRepository, ProductImageRepository>();
            unityContainer.RegisterType<ITopSellingProductsViewRepository, TopSellingProductsViewRepository>();
            unityContainer.RegisterType<IBrandRepository, BrandRepository>();
            unityContainer.RegisterType<IUnitRepository, UnitRepository>();
            unityContainer.RegisterType<IShelfRepository, ShelfRepository>();
            unityContainer.RegisterType<IWebSiteSliderRepository, WebSiteSliderRepository>();
            unityContainer.RegisterType<IProductVariationRepository, ProductVariationRepository>();
            unityContainer.RegisterType<IPurchaseBillRepository, PurchaseBillRepository>();
            unityContainer.RegisterType<ISupplierBankAccountRepository, SupplierBankAccountRepository>();
            unityContainer.RegisterType<ISupplierPaymentHistoryRepository, SupplierPaymentHistoryRepository>();
            unityContainer.RegisterType<IPaymentMethodRepository, PaymentMethodRepository>();
            unityContainer.RegisterType<IReturnedItemRepository, ReturnedItemRepository>();

        }
    }
}
