using System.Data.Entity;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{

    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    public sealed class WebSiteSliderRepository : BaseRepository<WebSiteSlider>, IWebSiteSliderRepository
    {
        public WebSiteSliderRepository(IUnityContainer container)
            : base(container)
        {

        }

        protected override IDbSet<WebSiteSlider> DbSet
        {
            get { return db.WebSiteSliders; }
        }
    }

    #endregion
}
