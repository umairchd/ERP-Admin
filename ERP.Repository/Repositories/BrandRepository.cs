using System.Data.Entity;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class BrandRepository: BaseRepository<Brand>, IBrandRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public BrandRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Brand> DbSet
        {
            get { return db.Brand; }
        }
        #endregion

        public Brand GetBrandWithName(string brandName)
        {
            return DbSet.FirstOrDefault(x => x.BrandName.Equals(brandName));
        }
    }
}
