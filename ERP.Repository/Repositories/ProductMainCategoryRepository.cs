using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class ProductMainCategoryRepository: BaseRepository<ProductMainCategory>, IProductMainCategoryRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductMainCategoryRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProductMainCategory> DbSet
        {
            get { return db.ProductMainCategory; }
        }
        #endregion

        public IEnumerable<ProductMainCategory> ProductMainCategoriesWeb()
        {
                return DbSet.Where(x=>x.IsWeb);
        }
    }
}
