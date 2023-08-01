using ERP.Models.DomainModels;

namespace ERP.Interfaces.IServices
{
    public interface IProductImageService
    {
        bool AddImage(ProductImage productImage);
        void DeleteAllImagesByProductId(long productId, bool isDefault);
        ProductImage ProductImage(long imageId);

    }
}
