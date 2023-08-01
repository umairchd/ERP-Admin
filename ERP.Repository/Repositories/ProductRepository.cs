using System;
using System.Collections.Generic;
using System.Configuration;
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
    public sealed class ProductRepository: BaseRepository<Product>, IProductRepository
    {
        #region Constructor & Private Properties
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Product> DbSet
        {
            get { return db.Products; }
        }

        readonly Dictionary<ProductByColumn, Func<Product, object>> requestClause =
           new Dictionary<ProductByColumn, Func<Product, object>>
                {
                    {ProductByColumn.ProductId, c => c.ProductId},
                    {ProductByColumn.Name, c => c.Name},
                    {ProductByColumn.CategoryTitle, c => c.ProductCategory.Name},
                    {ProductByColumn.BrandTitle, c => c.Brand.BrandName},
                };
        readonly Dictionary<WebsiteProductByColumn, Func<Product, object>> websiteRequestClause =
           new Dictionary<WebsiteProductByColumn, Func<Product, object>>
                {
                    //{WebsiteProductByColumn.Price, c => c.SalePrice},
                    {WebsiteProductByColumn.Name, c => c.Name},
                    //{WebsiteProductByColumn.Brand, c => c.ProductCategory.Name},
                };
        #endregion

        public ProductSearchResponse GetProductSearchResponse(ProductSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            long productVariationId;
            Int64.TryParse(searchRequest.ProductVariationId, out productVariationId);//try parse, because code may contains some special characters

            Expression<Func<Product, bool>> query =
                    s => ((
                                (string.IsNullOrEmpty(searchRequest.ProductVariationId) || productVariationId == 0 || s.ProductVariations.Any(x => x.ProductVariationId == productVariationId || x.Barcode == searchRequest.ProductVariationId))
                            && (searchRequest.ProductCode==0 || s.ProductId.Equals(searchRequest.ProductCode))
                            && (string.IsNullOrEmpty(searchRequest.ProductName) || s.Name.Contains(searchRequest.ProductName))
                            && (searchRequest.CategoryId == 0 || s.CategoryId.Equals(searchRequest.CategoryId))
                        ));
            IEnumerable<Product> result =
                searchRequest.IsAsc
                    ? DbSet.Include(x=>x.Brand).Include(x=>x.ProductCategory).Where(query)
                        .OrderBy(requestClause[searchRequest.ProductOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Include(x => x.Brand).Include(x => x.ProductCategory).Where(query)
                        .OrderByDescending(requestClause[searchRequest.ProductOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

            return new ProductSearchResponse { Products = result, TotalCount = DbSet.Count(), FilteredCount = DbSet.Count(query) };

        }

        //public Product GetProductByAnyCode(string code)
        //{
        //    long productId;
        //    Int64.TryParse(code,out productId);//try parse, because code may contains some special characters
        //    return DbSet.FirstOrDefault(x => x.ProductId == productId || x.ProductBarCode == code);
        //}
        //Added by Umer--<
        public IEnumerable<Product> GetProductsById(long id)
        {
            return DbSet.Where(x=>x.CategoryId.Equals(id));
        }

        public IEnumerable<Product> GetProductsByCategories(long mainCategoryId, long subCategoryId)
        {
            return DbSet.Where(x => x.CategoryId.Equals(subCategoryId));
        }
        public IEnumerable<Product> GetFeaturedProducts()
        {
            return DbSet.Take(8);
        }

        //public IEnumerable<Product> GetShortOnStockProducts()
        //{
        //    var result = DbSet.Where(x=>(x.OrderItems.Sum(y=>y.Quantity))>=x.MinimumStockLimit);
        //    return result;
        //}

        public WebsiteProductSearchResponse GetWebsiteProductSearchResponse(WebsiteProductSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<Product, bool>> query =
                s => ((string.IsNullOrEmpty(searchRequest.Keyword) || s.Name.Contains(searchRequest.Keyword)) && s.IsWeb.Equals(true) && (s.CategoryId.Equals(searchRequest.SubCategoryId)));
                
            IEnumerable<Product> result =
                searchRequest.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(websiteRequestClause[searchRequest.ProductOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(websiteRequestClause[searchRequest.ProductOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

            return new WebsiteProductSearchResponse { Products = result, TotalCount = DbSet.Count(x=>x.IsWeb && x.CategoryId.Equals(searchRequest.SubCategoryId)), FilteredCount = result.Count() };
        }

        public IEnumerable<Product> GetLatestProducts()
        {
            return DbSet.Where(x=>x.IsWeb.Equals(true)).OrderByDescending(x => x.RecCreatedDate).Take(int.Parse(ConfigurationManager.AppSettings["LatestProductCount"]));
        }
        
        //-->
    }
}
