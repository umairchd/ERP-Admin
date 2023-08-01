using System;
using System.Configuration;
using System.Linq;
using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class ProductVariationMapper
    {
        public static ProductVariation CreateFromClientToServer(this ProductVariationModel source)
        {
            return new ProductVariation
            {
                ProductVariationId = source.ProductVariationId,
                ProductId = source.ProductId,
                Barcode = source.Barcode,
                Purchaseprice = source.Purchaseprice,
                Saleprice = source.Saleprice,
                ShelfId = source.ShelfId,
                UnitId = source.UnitId,
                
                MinimumStockLimit = source.MinimumStockLimit,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }
        public static ProductVariation SetProductVariationCreatorUpdatorAndCreateFromClientToServer(this ProductVariationModel source, string userId)
        {
            return new ProductVariation
            {
                ProductVariationId = source.ProductVariationId,
                ProductId = source.ProductId,
                Barcode = source.Barcode,
                ProductVariantDescription = source.ProductVariantDescription,
                Purchaseprice = source.Purchaseprice,
                Saleprice = source.Saleprice,
                ShelfId = source.ShelfId,
                UnitId = source.UnitId,
                MinimumStockLimit = source.MinimumStockLimit,

                RecCreatedBy = source.RecCreatedBy ?? userId,
                RecCreatedDate = source.RecCreatedDate.Year == 1 ? DateTime.Now : source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy ?? userId,
                RecLastUpdatedDate = DateTime.Now
            };
        }


        public static ProductVariationModel CreateFromServerToClient(this ProductVariation source)
        {
            var pv= new ProductVariationModel
            {
                ProductVariationId = source.ProductVariationId,
                ProductId = source.ProductId,
                Barcode = source.Barcode,
                ProductVariantDescription = source.ProductVariantDescription,
                Purchaseprice = source.Purchaseprice,
                Saleprice = source.Saleprice,
                ShelfId = source.ShelfId,
                ShelfTitle = source.Shelf.Title,
                UnitId = source.UnitId,
                UnitTitle = source.Unit.UnitTitle,
                
                MinimumStockLimit = source.MinimumStockLimit,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                //ProductDefaultImageURL = ImageURL(source),
            };
            pv.Name = source.Product.Name+"-"+pv.UnitTitle;
            return pv;
        }
        
        public static ProductVariationModel CreateFromServerToClientList (this ProductVariation source)
        {
            return new ProductVariationModel
            {
                ProductVariationId = source.ProductVariationId,
                ProductId = source.ProductId,
                ShelfTitle = source.Shelf.Title,
                UnitTitle = source.Unit.UnitTitle,
                Name = source.Product.Name,
                CategoryTitle = source.Product.ProductCategory.Name,
                BrandTitle = source.Product.Brand.BrandName
            };
        }



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
    }
}
