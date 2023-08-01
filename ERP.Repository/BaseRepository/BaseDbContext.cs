using System.Data.Entity;
using ERP.Models.DomainModels;
using ERP.Models.LoggerModels;
using ERP.Models.MenuModels;
using Microsoft.Practices.Unity;

namespace ERP.Repository.BaseRepository
{
    /// <summary>
    /// Base Db Context. Implements Identity Db Context over Application User
    /// </summary>
    public sealed class BaseDbContext : DbContext
    {
        #region Private
        private IUnityContainer container;
        #endregion
        #region Constructor
        public BaseDbContext()
        {            
        }
        #endregion
        #region Public

        public BaseDbContext(string connectionString,IUnityContainer container)
            : base(connectionString)
        {
            this.container = container;
        }
        //#region Logger

        /// <summary>
        /// Logs
        /// </summary>
        public DbSet<Log> Logs { get; set; }
        /// <summary>
        /// Log Categories
        /// </summary>
        public DbSet<LogCategory> LogCategories { get; set; }
        /// <summary>
        /// Category Logs
        /// </summary>
        public DbSet<CategoryLog> CategoryLogs { get; set; }
        #endregion
        #region Menu Rights and Security
        /// <summary>
        /// Menu Rights
        /// </summary>
        public DbSet<MenuRight> MenuRights { get; set; }
        /// <summary>
        /// Menu
        /// </summary>
        public DbSet<Menu> Menus { get; set; }
        #endregion
        /// <summary>
        /// Users
        /// </summary>
        public DbSet<AspNetUser> Users { get; set; }

        /// <summary>
        /// User Roles
        /// </summary>
        public DbSet<AspNetRole> UserRoles { get; set; }
        public DbSet<AspNetUserClaim> UserClaims { get; set; }
        public DbSet<AspNetUserLogin> UserLogins { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PurchaseBillItem> InventoryItems { get; set; }
        public DbSet<ProductConfiguration> ProductConfiguration { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ProductMainCategory> ProductMainCategory { get; set; }
        public DbSet<NotesCategory> NotesCategories { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ProductsStock> ProductsStocks { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<TopSellingProductsView> TopSellingProductsView { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Shelf> Shelf { get; set; }
        public DbSet<WebSiteSlider> WebSiteSliders { get; set; }
        public DbSet<ProductVariation> ProductVariation { get; set; }
        public DbSet<PurchaseBill> PurchaseBill { get; set; }
        public DbSet<SupplierBankAccount> SupplierBankAccount { get; set; }
        public DbSet<SupplierPaymentHistory> SupplierPaymentHistories { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<ReturnedOrderItem> ReturnedOrderItems { get; set; }
    }
}
