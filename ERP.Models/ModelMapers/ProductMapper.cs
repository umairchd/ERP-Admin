using System.Configuration;
using System.Linq;
using ERP.Models.DomainModels;
using ERP.Models.WebModels;
using ERP.Models.WebViewModels;
using ProductImage = ERP.Models.WebModels.ProductImage;

namespace ERP.Models.ModelMapers
{
    public static class ProductMapper
    {
        #region ProductMapper
        public static Product CreateFromClientToServer(this ProductModel source)
        {
            var product= new Product
            {
                CategoryId = source.CategoryId,
                ProductId = source.ProductId,
                Name = source.Name,
                ProductDescription = source.ProductDescription,
                Comments = source.Comments,
                IsWeb = source.IsWeb,
                BrandId = source.BrandId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };

            return product;
        }

        public static ProductModel CreateFromServerToClient(this Product source)
        {
            return new ProductModel
            {
                CategoryId = source.CategoryId,
                ProductId = source.ProductId,
                Name = source.Name,
                ProductDescription = source.ProductDescription,
                Comments = source.Comments,
                IsWeb = source.IsWeb,
                BrandId = source.BrandId,
                CategoryName = source.ProductCategory.Name,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
            };
        }
        public static ProductModel CreateFromServerToClientList(this Product source)
        {
            return new ProductModel
            {
                CategoryId = source.CategoryId,
                ProductId = source.ProductId,
                Name = source.Name,
                Comments = source.Comments,
                IsWeb = source.IsWeb,
                CategoryName = source.ProductCategory.Name,
                BrandName = source.Brand.BrandName
            };
        }
        #endregion
        #region ProductApiMapper
        public static ProductApiModel CreateApiModelServerToClient(this ProductVariation source)
        {
            return new ProductApiModel
            {
                CategoryId = source.Product.CategoryId,
                ProductId = source.ProductVariationId,
                ProductBarCode = source.Barcode,
                Name = source.Product.Name,
                PurchasePrice = source.Purchaseprice,
                SalePrice = source.Saleprice,
                //MinSalePriceAllowed = source.Saleprice,
                Comments = source.Product.Comments,
                RecCreatedBy = source.RecCreatedBy,
                //MinimumStockLimit = source.MinimumStockLimit,
                RecCreatedDate = source.RecCreatedDate,
                BrandId = source.Product.BrandId,
                IsWeb = source.Product.IsWeb,
                ProductDefaultImageURL = ImageURL(source),
            };
        }
        //Added by Umer--<
        public static ProductWebApiModel MapProductServerToClient(this Product product)
        {
            return new ProductWebApiModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                //SalePrice = product.Saleprice,
                Description = product.ProductDescription,

                ImageUrl = ImageURLWebsite(product),

            };
        }
        public static ProductWebApiModel MapProductVariationServerToClient(this ProductVariation product)
        {
            return new ProductWebApiModel
            {
                ProductId = product.ProductVariationId,
                Name = product.Product.Name,
                SalePrice = product.Saleprice,
                Description = product.Product.ProductDescription,

                ImageUrl = ImageURLWebsite(product),
                
            };
        }
        public static ProductWebApiModel MapProductVariationServerToClientForBarcode(this ProductVariation product)
        {
            return new ProductWebApiModel
            {
                ProductId = product.ProductId,
                Name = product.Product.Name + " - "+product.Saleprice,
                SalePrice = product.Saleprice,
                Description = product.Product.ProductDescription,
                Barcode = product.Barcode,
            };
        }
        public static ProductWebApiModel MapProductServerToClientForSaleInvoice(this ProductVariation product)
        {
            return new ProductWebApiModel
            {
                ProductId = product.ProductId,
                ProductName = product.Product.Name + "-" + product.Unit.UnitTitle,
                SalePrice = product.Saleprice,
                Barcode = product.Barcode ?? product.ProductId.ToString(),
            };
        }
        public static ProductWebApiModel MapProductServerToClient(this TopSellingProductsView product)
        {
            return new ProductWebApiModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                SalePrice = product.SalePrice,
                Description = "",
                ImageUrl = product.ImageId > 0 ? ConfigurationManager.AppSettings["HostURL"] + ConfigurationManager.AppSettings["ShopIndexDefaultImageUrl"] + product.ImageId : ConfigurationManager.AppSettings["HostURL"] + ConfigurationManager.AppSettings["DefaultProductImageUrl"],
            };
        }
        #endregion
        #region ProductImageMapper
        public static ProductImage MapProductImageServerToClient(this DomainModels.ProductImage image)
        {
            return new ProductImage()
            {
                ProductVariationId = image.ProductVariationId,
                ImageId = image.ImageId,
                ImageData = image.ImageData,
                IsDefault = image.IsDefault,
                ContentType = image.ContentType,
                RecCreatedBy = image.RecCreatedBy,
                RecCreatedDate = image.RecCreatedDate,
                RecLastUpdatedBy = image.RecLastUpdatedBy,
                RecLastUpdatedDate = image.RecLastUpdatedDate,
                
            };
        }
        public static DomainModels.ProductImage MapProductImageClientToServer(this ProductImage image)
        {
            return new DomainModels.ProductImage()
            {
                ProductVariationId = image.ProductVariationId,
                ImageId = image.ImageId,
                ImageData = image.ImageData,
                ContentType = image.ContentType,
                IsDefault = image.IsDefault,
                RecCreatedBy = image.RecCreatedBy,
                RecCreatedDate = image.RecCreatedDate,
                RecLastUpdatedBy = image.RecLastUpdatedBy,
                RecLastUpdatedDate = image.RecLastUpdatedDate,
            };
        }
        #endregion

        public static string ImageURL(ProductVariation product)
        {
            string URL = string.Empty;
            if (product.Product.IsWeb.Equals(true))
            {
                var image = product.ProductImages.FirstOrDefault(x => x.IsDefault.Equals(true));

                URL = image != null ? ConfigurationManager.AppSettings["HostURL"] + ConfigurationManager.AppSettings["ShopProductsDefaultImageUrl"] + image.ImageId : "";
            }
            return URL;
        }
        public static string ImageURLWebsite(ProductVariation product)
        {
            string URL = string.Empty;
            if (product.Product.IsWeb.Equals(true))
            {
                var productImage = product.ProductImages.FirstOrDefault(x => x.IsDefault.Equals(true));

                var hostUrl = ConfigurationManager.AppSettings["HostURL"] + ConfigurationManager.AppSettings["ShopIndexDefaultImageUrl"];
                URL = productImage != null ? hostUrl + productImage.ImageId : ConfigurationManager.AppSettings["HostURL"] + ConfigurationManager.AppSettings["DefaultProductImageUrl"];
            }
            return URL;
        }
        public static string ImageURLWebsite(Product product)
        {
            string URL = string.Empty;
            if (product.IsWeb.Equals(true))
            {
                var productImage = product.ProductVariations.FirstOrDefault().ProductImages.FirstOrDefault(x => x.IsDefault.Equals(true));

                var hostUrl = ConfigurationManager.AppSettings["HostURL"] + ConfigurationManager.AppSettings["ShopIndexDefaultImageUrl"];
                URL = productImage != null ? hostUrl + productImage.ImageId : ConfigurationManager.AppSettings["HostURL"] + ConfigurationManager.AppSettings["DefaultProductImageUrl"];
            }
            return URL;
        }

        //-->
    }
}