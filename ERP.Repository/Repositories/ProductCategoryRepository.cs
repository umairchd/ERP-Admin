using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class ProductCategoryRepository: BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductCategoryRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProductCategory> DbSet
        {
            get { return db.ProductCategories; }
        }
        #endregion

        public IEnumerable<ProductCategory> GetAllSubCategoriesByMainCategoryId(long mainCategoryId)
        {
            return DbSet.Where(x => x.ParentCategoryId == mainCategoryId).OrderBy(x=>x.Name);
        }
        public IEnumerable<ProductCategory> GetAllSubCategories()
        {
            return DbSet.Where(x => x.ParentCategoryId != null).OrderBy(x => x.Name); 
        }
        public IEnumerable<ProductCategory> GetAllMainCategories()
        {
            return DbSet.Where(x => x.ParentCategoryId == null).OrderBy(x => x.Name); ;
        }
    }
}
