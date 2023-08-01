using System.Data.Entity;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class UnitRepository: BaseRepository<Unit>, IUnitRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public UnitRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Unit> DbSet
        {
            get { return db.Unit; }
        }
        #endregion

    }
}
