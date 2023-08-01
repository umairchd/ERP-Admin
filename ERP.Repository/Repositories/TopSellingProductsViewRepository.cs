using System.Collections.Generic;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class TopSellingProductsViewRepository : BaseRepository<TopSellingProductsView>, ITopSellingProductsViewRepository
    {
        #region Constructor
        public TopSellingProductsViewRepository(IUnityContainer container)
            : base(container)
        {

        }
        protected override System.Data.Entity.IDbSet<TopSellingProductsView> DbSet
        {
            get { return db.TopSellingProductsView; }
        }
        #endregion

        public IEnumerable<TopSellingProductsView> GetTopSellingProducts()
        {
            //return DbSet.OrderByDescending(x => x.saleQuantity).Take(int.Parse(ConfigurationManager.AppSettings["TopSellingProductsCount"]));
            return new List<TopSellingProductsView>();
        }
    }
}
