using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
    public class ProductImageService:IProductImageService
    {
        private readonly IProductImageRepository productImageRepository;

        public ProductImageService(IProductImageRepository productImageRepository)
        {
            this.productImageRepository = productImageRepository;
        }

        public bool AddImage(ProductImage productImage)
        {
            var image = productImageRepository.FindImageByProductId(productImage.ProductVariationId);
            if(image!=null)
            productImageRepository.Delete(image);
            productImageRepository.Add(productImage);
            productImageRepository.SaveChanges();
            return true;
        }

        public void DeleteAllImagesByProductId(long productId, bool isDefault)
        {
            productImageRepository.DeleteAllImagesByProductId(productId, isDefault);
        }

        public ProductImage ProductImage(long imageId)
        {
            return productImageRepository.Find(imageId);
        }
    }
}
