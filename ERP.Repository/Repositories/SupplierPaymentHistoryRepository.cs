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
    public sealed class SupplierPaymentHistoryRepository: BaseRepository<SupplierPaymentHistory> , ISupplierPaymentHistoryRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SupplierPaymentHistoryRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SupplierPaymentHistory> DbSet
        {
            get { return db.SupplierPaymentHistories; }
        }
        #endregion

        #region Private Properties
        readonly Dictionary<SupplierPaymentHistoryByColumn, Func<SupplierPaymentHistory, object>> requestClause =
           new Dictionary<SupplierPaymentHistoryByColumn, Func<SupplierPaymentHistory, object>>
                {
                    {SupplierPaymentHistoryByColumn.SupplierPaymentHistoryId, c => c.SupplierPaymentHistoryId},
                    {SupplierPaymentHistoryByColumn.Vendor, c => c.SupplierId},
                    {SupplierPaymentHistoryByColumn.PaymentDate, c => c.RecCreatedDate},
                    {SupplierPaymentHistoryByColumn.Amount, c => c.Amount}
                };
        #endregion

        public SupplierPaymentHistorySearchResponse GetPaymentHistorySearchResponse(SupplierPaymentHistorySearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            //if (searchRequest.Vendor != null)
            //    searchRequest.Vendor = searchRequest.Vendor.Value;
            Expression<Func<SupplierPaymentHistory, bool>> query =
                    x => (
                             (searchRequest.Vendor == null || x.SupplierId == searchRequest.Vendor.Value) &&
                ((searchRequest.PaymentDateFrom == null || (DbFunctions.TruncateTime(searchRequest.PaymentDateFrom.Value).Value <= DbFunctions.TruncateTime(x.RecCreatedDate))) &&
                (searchRequest.PaymentDateTo == null || (DbFunctions.TruncateTime(searchRequest.PaymentDateTo.Value).Value >= DbFunctions.TruncateTime(x.RecCreatedDate))))
                        );
            IEnumerable<SupplierPaymentHistory> result =
                searchRequest.IsAsc
                    ? DbSet.Include(x=>x.SupplierBankAccount).Include(x=>x.Supplier).Where(query)
                        .OrderBy(requestClause[searchRequest.SupplierPaymentHistoryByColumn])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Include(x=>x.SupplierBankAccount).Include(x=>x.Supplier).Where(query)
                        .OrderByDescending(requestClause[searchRequest.SupplierPaymentHistoryByColumn])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

            return new SupplierPaymentHistorySearchResponse { PaymentHistoryList = result, TotalCount = DbSet.Count(), FilteredCount = DbSet.Count(query) };
        }

        public SupplierPaymentHistory GetSupplierPaymentHistoriesBySupplierId(long purchaseBillId)
        {
            return DbSet.FirstOrDefault(x => x.PurchaseBillId == purchaseBillId);
        }
    }
}
