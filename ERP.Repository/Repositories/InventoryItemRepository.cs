using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ERP.Interfaces.IRepository;
using ERP.Models.Common;
using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class PurchaseBillItemRepository: BaseRepository<PurchaseBillItem>, IPurchaseBillItemRepositoy
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PurchaseBillItemRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PurchaseBillItem> DbSet
        {
            get { return db.InventoryItems; }
        }

        readonly Dictionary<InventoryItemByColumn, Func<PurchaseBillItem, object>> requestClause =
           new Dictionary<InventoryItemByColumn, Func<PurchaseBillItem, object>>
                {
                    {InventoryItemByColumn.Code, c => c.ProductVariationId},
                    {InventoryItemByColumn.Name, c => c.ProductVariation.Product.Name},
                    {InventoryItemByColumn.Brand, c => c.ProductVariation.Product.Brand.BrandName},
                    {InventoryItemByColumn.Unit, c => c.ProductVariation.Unit.UnitTitle},
                    {InventoryItemByColumn.Quantity, c => c.Quantity},
                    {InventoryItemByColumn.SalePrice, c => c.SalePrice},
                    //{InventoryItemByColumn.Vendor, c => c.Vendor.Name}
                };
        #endregion

        public long GetItemCountInInventory(long productId)
        {
            return DbSet.Where(x => x.ProductVariationId == productId).Select(r => r.Quantity).DefaultIfEmpty(0).Sum();
        }

        public InventoryItemSearchResponse GetInventoryItemSearchResponse(InventoryItemSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<PurchaseBillItem, bool>> query =
                    s => (
                            (
                            (searchRequest.ProductCode == 0 || s.ProductVariationId.Equals(searchRequest.ProductCode))
                            //&& (searchRequest.Vendor == 0 || s.VendorId.Equals(searchRequest.Vendor))
                            && (string.IsNullOrEmpty(searchRequest.Barcode) || s.ProductVariation.Barcode.Contains(searchRequest.Barcode))
                            && (string.IsNullOrEmpty(searchRequest.Name) || s.ProductVariation.Product.Name.Contains(searchRequest.Name))
                            //&& (searchRequest.PurchaseDate == null || DbFunctions.TruncateTime(s.PurchasingDate)==searchRequest.PurchaseDate)
                            )
                        );
            IEnumerable<PurchaseBillItem> result =
                searchRequest.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(requestClause[searchRequest.ItemOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(requestClause[searchRequest.ItemOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

            return new InventoryItemSearchResponse { InventoryItems = result, TotalCount = DbSet.Count(), FilteredCount = DbSet.Count(query) };
        }

        public IEnumerable<PurchaseBillItem> PurchaseReport(string productCode, long vendorId, DateTime startDate, DateTime endDate)
        {
            long productId;
            long.TryParse(productCode, out productId);
            var stDt = new DateTime(2001, 1, 1);
            var enDt = new DateTime(2001, 1, 1);
            return DbSet.Where(x => (productId == 0 || productId == x.ProductVariationId) && (vendorId==0 || x.PurchaseBill.VendorId==vendorId) && (startDate==stDt ||(DbFunctions.TruncateTime(x.PurchaseBill.RecCreatedDate) >= DbFunctions.TruncateTime(startDate))) && (endDate==enDt||(DbFunctions.TruncateTime(x.PurchaseBill.RecCreatedDate) <= DbFunctions.TruncateTime(endDate)))).ToList();
        }
    }
}
