using System.Data.Entity;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class NoteRepository: BaseRepository<Note>, INoteRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public NoteRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Note> DbSet
        {
            get { return db.Notes; }
        }
        #endregion        
    }
}
