using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class InventoryItemMapper
    {
        public static PurchaseBillItem CreateFromClientToServer(this InventoryItemModel source)
        {
            return new PurchaseBillItem
            {
                ItemId = source.ItemId,
                ProductVariationId = source.ProductVariationId,
                Quantity = source.Quantity,
                //VendorId = source.VendorId,
                //Comments = source.Comments,
                //MinSalePriceAllowed = source.MinSalePriceAllowed,
                PurchasePrice = source.PurchasePrice,
                SalePrice = source.SalePrice,
                //PurchasingDate = source.PurchasingDate,
                //RecCreatedBy = source.RecCreatedBy,
                //RecCreatedDate = source.RecCreatedDate,
                //RecLastUpdatedBy = source.RecLastUpdatedBy,
                //RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }

        public static InventoryItemModel CreateFromServerToClient(this PurchaseBillItem source)
        {
            return new InventoryItemModel
            {
                ItemId = source.ItemId,
                ProductVariationId = source.ProductVariationId,
                Quantity = source.Quantity,
                //VendorId = source.VendorId,
                //Comments = source.Comments,
                //MinSalePriceAllowed = source.MinSalePriceAllowed,
                PurchasePrice = source.PurchasePrice,
                SalePrice = source.SalePrice,
                //PurchasingDate = source.PurchasingDate,
                //RecCreatedBy = source.RecCreatedBy,
                //RecCreatedDate = source.RecCreatedDate,
                //RecLastUpdatedBy = source.RecLastUpdatedBy,
                //RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }

        public static InventoryItemModel MapPurchaseBillItem(this PurchaseBillItem source)
        {
            return new InventoryItemModel
            {
                ItemId = source.ItemId,
                ProductVariationId = source.ProductVariationId,
                Quantity = source.Quantity,
                PurchaseBillId = source.PurchaseBillId,
                Barcode = source.ProductVariation.Barcode,
                ProductName = source.ProductVariation.Product.Name,
                UnitTitle = source.ProductVariation.Unit.UnitTitle,
                PurchasePrice = source.PurchasePrice,
                SalePrice = source.SalePrice
            };
        }

        public static InventoryItemListModel CreateListServerToClient(this PurchaseBillItem source)
        {
            return new InventoryItemListModel
            {
                ItemId = source.ItemId,
                ProductVariationId = source.ProductVariationId,
                ProductName = source.ProductVariation.Product.Name,
                Quantity = source.Quantity,
                //VendorId = source.VendorId,
                //VendorName = source.Vendor.Name,
                BrandTitle = source.ProductVariation.Product.Brand.BrandName,
                UnitTitle = source.ProductVariation.Unit.UnitTitle,
                //PurchasePrice = source.PurchasePrice,
                SalePrice = source.SalePrice,
                //PurchasingDate = source.PurchasingDate.ToShortDateString()
            };
        }
    }
}