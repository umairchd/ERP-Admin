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
    public sealed class ReturnedItemRepository: BaseRepository<ReturnedOrderItem>, IReturnedItemRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ReturnedItemRepository(IUnityContainer container)
            : base(container)
        {
            
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ReturnedOrderItem> DbSet
        {
            get { return db.ReturnedOrderItems; }
        }

        readonly Dictionary<ReturnItemByColumn, Func<ReturnedOrderItem, object>> requestClause =
          new Dictionary<ReturnItemByColumn, Func<ReturnedOrderItem, object>>
                {
                    {ReturnItemByColumn.OrderId, c => c.OrderId},
                    {ReturnItemByColumn.ItemCode, c => c.ProductVariationId},
                    {ReturnItemByColumn.Price, c => c.Price},
                    {ReturnItemByColumn.ReturnDate, c => c.ReceivedDate},
                    {ReturnItemByColumn.TotalItems, c => c.ReturnedQty},
                    {ReturnItemByColumn.RecievedBy, c => c.ReceivedBy},
                };
        #endregion

        public ReturnItemsSearchResponse GetReturnItemsSearchResponse(ReturnItemSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<ReturnedOrderItem, bool>> query =
                    s => (
                            (
                            (string.IsNullOrEmpty(searchRequest.ProductCode) || s.ProductVariationId.ToString() == searchRequest.ProductCode)
                            && (string.IsNullOrEmpty(searchRequest.OrderId) || s.OrderId.ToString().Equals(searchRequest.OrderId))
                            && (searchRequest.ReturnDateFrom == null || 
                            DbFunctions.TruncateTime(s.ReceivedDate) >= DbFunctions.TruncateTime(searchRequest.ReturnDateFrom.Value))
                            && (searchRequest.ReturnDateTo == null ||
                            DbFunctions.TruncateTime(s.ReceivedDate) <= DbFunctions.TruncateTime(searchRequest.ReturnDateTo.Value))
                            )
                        );
            IEnumerable<ReturnedOrderItem> result =
                searchRequest.IsAsc
                    ? DbSet.Include(x => x.ProductVariation).Include(x => x.AspNetUser).Where(query)
                        .OrderBy(requestClause[searchRequest.ReturnItemOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Include(x => x.ProductVariation).Include(x => x.AspNetUser).Where(query)
                        .OrderByDescending(requestClause[searchRequest.ReturnItemOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

            return new ReturnItemsSearchResponse { ReturnedOrderItems = result, TotalCount = DbSet.Count(), FilteredCount = DbSet.Count(query) };
        }

        public ReturnedOrderItem GetReturnedOrderItem(long returnId)
        {
            return DbSet.FirstOrDefault(x => x.ReturnId.Equals(returnId));
        }
    }
}
