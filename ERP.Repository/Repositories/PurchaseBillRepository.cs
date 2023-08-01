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
    public sealed class PurchaseBillRepository: BaseRepository<PurchaseBill>, IPurchaseBillRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PurchaseBillRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PurchaseBill> DbSet
        {
            get { return db.PurchaseBill; }
        }
        #endregion

        #region Private Properties
        readonly Dictionary<PurchaseBillByColumn, Func<PurchaseBill, object>> requestClause =
           new Dictionary<PurchaseBillByColumn, Func<PurchaseBill, object>>
                {
                    {PurchaseBillByColumn.BillId, c => c.PurchaseBillId},
                    {PurchaseBillByColumn.PaidAmount, c => c.PaidAmount},
                    {PurchaseBillByColumn.PuchaseDate, c => c.PurchaseDate},
                    {PurchaseBillByColumn.SupplierName, c => c.Supplier.Name},
                    {PurchaseBillByColumn.SupplierInvcNo, c => c.VendorInvoiceNo},
                    {PurchaseBillByColumn.GrossTotal, c => c.PurchaseBillItems.Sum(x=>x.PurchasePrice)},
                };
        #endregion

        public PurchaseBill GetPurchaseBill(long purchaseBillId)
        {
            return DbSet.Include(x => x.PurchaseBillItems).FirstOrDefault(x => x.PurchaseBillId.Equals(purchaseBillId));
        }

        public PurchaseBillsSearchResponse SearchPurchaseBills(PuchaseBillsSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<PurchaseBill, bool>> query =
                    s => (
                            (
                            (searchRequest.PurchaseDateFrom == null || 
                            (DbFunctions.TruncateTime(searchRequest.PurchaseDateFrom.Value).Value <= DbFunctions.TruncateTime(s.PurchaseDate)))
                            &&(searchRequest.PurchaseDateTo == null  || 
                            DbFunctions.TruncateTime(searchRequest.PurchaseDateTo) >= DbFunctions.TruncateTime(s.PurchaseDate))
                            && 
                            (searchRequest.Vendor == null ||s.VendorId==searchRequest.Vendor)
                            )
                        );
            IEnumerable<PurchaseBill> result =
                searchRequest.IsAsc
                    ? DbSet.Include(x=>x.PurchaseBillItems).Include(x=>x.Supplier).Where(query)
                        .OrderBy(requestClause[searchRequest.PurchaseBillByColumn])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Include(x => x.PurchaseBillItems).Include(x => x.Supplier).Where(query)
                        .OrderByDescending(requestClause[searchRequest.PurchaseBillByColumn])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

            return new PurchaseBillsSearchResponse { PurchaseBills = result, TotalCount = DbSet.Count(), FilteredCount = DbSet.Count(query) };
        }

        public decimal GetFilteredBills(SupplierPaymentHistorySearchRequest searchRequest)
        {
            return DbSet
                .Where(x =>
                    (searchRequest.Vendor == null || x.VendorId == searchRequest.Vendor.Value) && 
                ((searchRequest.PaymentDateFrom == null || (DbFunctions.TruncateTime(searchRequest.PaymentDateFrom.Value).Value <= DbFunctions.TruncateTime(x.RecCreatedDate))) && 
                (searchRequest.PaymentDateTo == null || (DbFunctions.TruncateTime(searchRequest.PaymentDateTo.Value).Value >= DbFunctions.TruncateTime(x.RecCreatedDate)))
                )).DefaultIfEmpty()
                .Sum(y => y.PurchaseBillItems.Select(c => c.PurchasePrice).DefaultIfEmpty().Sum());
        }
    }
}
