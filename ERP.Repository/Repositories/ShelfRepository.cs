using System.Data.Entity;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class ShelfRepository: BaseRepository<Shelf>, IShelfRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ShelfRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Shelf> DbSet
        {
            get { return db.Shelf; }
        }
        #endregion

    }
}
