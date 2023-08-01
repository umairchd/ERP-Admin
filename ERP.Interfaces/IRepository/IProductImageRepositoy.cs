using ERP.Models.DomainModels;

namespace ERP.Interfaces.IRepository
{
    public interface IProductImageRepository : IBaseRepository<ProductImage, long>
    {
        
        //Added by Umer--<     
        void DeleteAllImagesByProductId(long productId, bool isDefault);
        ProductImage FindImageByProductId(long id);
        //-->

    }
}
