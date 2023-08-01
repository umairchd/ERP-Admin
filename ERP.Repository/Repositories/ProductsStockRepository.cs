using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class ProductsStockRepository: BaseRepository<ProductsStock>, IProductsStockRepository    
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductsStockRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProductsStock> DbSet
        {
            get { return db.ProductsStocks; }
        }
        #endregion

        public IEnumerable<ProductsStock> StocksReport(string barCode, string productCode, string productName)
        {
            return DbSet.Where(x => ((string.IsNullOrEmpty(productCode) || x.ProductId.ToString() == productCode)) && (string.IsNullOrEmpty(productName) || x.Name.Contains(productName))).ToList();
        }
    }
}
