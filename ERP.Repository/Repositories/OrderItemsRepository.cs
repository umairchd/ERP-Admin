using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class OrderItemsRepository: BaseRepository<OrderItem>, IOrderItemsRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderItemsRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<OrderItem> DbSet
        {
            get { return db.OrderItems; }
        }
        #endregion

        public long GetItemCountInOrders(long productId)
        {
            return DbSet.Where(x => x.ProductVariationId == productId && (x.Order.IsDeleted != true)).Select(r => r.Quantity).DefaultIfEmpty(0).Sum();
        }

        public IEnumerable<OrderItem> GetOrderItemsReport(string productCode,DateTime startDate, DateTime endDate)
        {
            long productId;
            long.TryParse(productCode, out productId);
            return DbSet.Where(x => (productId == 0 || productId == x.ProductVariationId) 
                && (DbFunctions.TruncateTime(x.Order.RecCreatedDate) >= DbFunctions.TruncateTime(startDate))
                && (DbFunctions.TruncateTime(x.Order.RecCreatedDate) <= DbFunctions.TruncateTime(endDate))
                && (x.Order.IsDeleted != true)
                ).ToList();
        }

        public void GetDashboardWidgetsData(DateTime dateTime)
        {
            var yesterday = dateTime.AddDays(-1);
            var data = DbSet.GroupBy(x => x.ProductVariationId).Select(item => new
            {
                TodayTotalSale = item.Where(x => x.Order.RecCreatedDate <= dateTime && x.Order.RecCreatedDate > yesterday).Sum(y => ((y.SalePrice * y.Quantity) - y.Discount)),
                TodayProfit = item.Where(x => x.Order.RecCreatedDate <= dateTime && x.Order.RecCreatedDate > yesterday).Sum(y => (((y.SalePrice - y.PurchasePrice) * y.Quantity) - y.Discount))
            });
        }

        public OrderItem GetItemByProductVariationId(long productVariationId, long orderId)
        {
            return DbSet.FirstOrDefault(x => x.OrderId == orderId && x.ProductVariationId == productVariationId);
        }

        
    }
}
