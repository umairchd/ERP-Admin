using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class SupplierRepository: BaseRepository<Supplier>, IVendorRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SupplierRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Supplier> DbSet
        {
            get { return db.Supplier; }
        }
        #endregion

        public IEnumerable<Supplier> GetActiveVendors()
        {
            return DbSet.Where(x => x.ActiveFlag);
        }
    }
}
