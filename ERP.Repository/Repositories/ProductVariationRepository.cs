using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ERP.Interfaces.IRepository;
using ERP.Models.ApiModels.Response;
using ERP.Models.Common;
using ERP.Models.DomainModels;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class ProductVariationRepository: BaseRepository<ProductVariation>, IProductVariationRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductVariationRepository(IUnityContainer container)
            : base(container)
        {
        }

        readonly Dictionary<ProductVariationByColumn, Func<ProductVariation, object>> requestClause =
           new Dictionary<ProductVariationByColumn, Func<ProductVariation, object>>
                {
                    {ProductVariationByColumn.ProductId, c => c.ProductId},
                    {ProductVariationByColumn.ItemId, c => c.ProductVariationId},
                    {ProductVariationByColumn.Name, c => c.Product.Name},
                    {ProductVariationByColumn.CategoryTitle, c => c.Product.ProductCategory.Name},
                    {ProductVariationByColumn.BrandTitle, c => c.Product.Brand.BrandName},
                    {ProductVariationByColumn.UnitTitle, c => c.Unit.UnitTitle},
                    {ProductVariationByColumn.ShelfTitle, c => c.Shelf.ShelfId}
                };

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProductVariation> DbSet
        {
            get { return db.ProductVariation; }
        }
        #endregion

        public ProductVariation GetProductByAnyCode(string code)
        {
            long productId;
            Int64.TryParse(code, out productId);//try parse, because code may contains some special characters
            return DbSet.FirstOrDefault(x => x.ProductVariationId == productId || x.Barcode == code);
        }
        public IEnumerable<ProductVariation> GetShortOnStockProducts()
        {
            var result = DbSet.Where(x => (x.OrderItems.Sum(y => y.Quantity)) >= x.MinimumStockLimit);
                return result;
        }
        public IEnumerable<ProductVariation> GetProductVariationsByCategories(long mainCategoryId, long subCategoryId)
        {
            return DbSet.Where(x => x.Product.CategoryId.Equals(subCategoryId));
        }
        public ProductVariationSearchResponse GetProductVariationSearchResponse(ProductVariationSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<ProductVariation, bool>> query =
                    s => (
                            (
                            (searchRequest.ProductCode == 0 || s.ProductId.Equals(searchRequest.ProductCode) || s.ProductVariationId.Equals(searchRequest.ProductCode))
                            && (string.IsNullOrEmpty(searchRequest.Barcode) || s.Barcode.Equals(searchRequest.Barcode))
                            && (string.IsNullOrEmpty(searchRequest.Name) || s.Product.Name.Contains(searchRequest.Name))
                            && (searchRequest.Category == 0 || s.Product.CategoryId.Equals(searchRequest.Category)))
                        );
            IEnumerable<ProductVariation> result =
                searchRequest.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(requestClause[searchRequest.ProductOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(requestClause[searchRequest.ProductOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

            return new ProductVariationSearchResponse { ProductVariations = result, TotalCount = DbSet.Count(), FilteredCount = DbSet.Count(query) };
        }
        public ProductVariationStockApiResponse GetProductVariationStock(string code)
        {
            long productVariationId;
            Int64.TryParse(code,out productVariationId);//try parse, because code may contains some special characters

            var response=DbSet.Where(x=>x.ProductVariationId.Equals(productVariationId) || x.Barcode.Equals(code)).Select(item=>new ProductVariationStockApiResponse
            {
                ProductVariationId = item.ProductVariationId,
                Barcode = item.Barcode,
                AvailableQuantity = item.PurchaseBillItems.Select(x => x.Quantity).DefaultIfEmpty().Sum() - item.OrderItems.Select(y => y.Quantity).DefaultIfEmpty().Sum(),
                PurchasePrice = item.Purchaseprice,
                SalePrice = item.Saleprice,
                BrandName = item.Product.Brand.BrandName,
                ProductName = item.Product.Name,
                UnitTitle = item.Unit.UnitTitle
            }).FirstOrDefault();
            return response;
        }
    }
}
