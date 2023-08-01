using System.Data.Entity;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class ProductImageRepository: BaseRepository<ProductImage>, IProductImageRepository
    {
        #region Constructor & Private Properties
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductImageRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ProductImage> DbSet
        {
            get { return db.ProductImages; }
        }

        #endregion

        public void DeleteAllImagesByProductId(long productId, bool isDefault)
        {
            var imagesToDelete = DbSet.Where(x => x.IsDefault==isDefault && x.ProductVariationId == productId);
            db.ProductImages.RemoveRange(imagesToDelete);
        }

        public ProductImage FindImageByProductId(long id)
        {
            return DbSet.FirstOrDefault(x => x.ProductVariationId.Equals(id) && x.IsDefault.Equals(true));
        }
    }
}
