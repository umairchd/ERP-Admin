using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class OrderItemMapper
    {

        public static OrderItemModelWithProduct CreateFromServerToClientWithProductDetails(this OrderItem source)
        {
            return new OrderItemModelWithProduct
            {
                OrderItemId = source.OrderItemId,
                OrderId =  source.OrderId,
                //AmountGiven = source.AmountGiven,
                Discount = source.Discount,
                PurchasePrice = source.PurchasePrice,
                ProductId = source.ProductVariationId,//ProductId actually is ProductVariationId
                SalePrice = source.SalePrice,
                //MinSalePriceAllowed = source.MinSalePriceAllowed,
                Quantity = source.Quantity,
                

                //Comments = source.Comments,
                //RecCreatedBy = source.RecCreatedBy,
                //RecCreatedDate = source.RecCreatedDate,
                //RecLastUpdatedBy = source.RecLastUpdatedBy,
                //RecLastUpdatedDate = source.RecLastUpdatedDate,
                Product = source.ProductVariation.CreateFromServerToClient()
               
            };
        }
        public static OrderItemForApi CreatePureOrderItemForAPI(this OrderItem source)
        {
            return new OrderItemForApi
            {
                OrderItemId = source.OrderItemId,
                OrderId = source.OrderId,
                Discount = source.Discount,
                PurchasePrice = source.PurchasePrice,
                ProductVariationId = source.ProductVariationId,//ProductId actually is ProductVariationId
                SalePrice = source.SalePrice,
                Quantity = source.Quantity
            };
        }
        public static OrderItem CreateFromClientToServer(this OrderItemModelWithProduct source,long orderId)
        {
            return new OrderItem
            {
                OrderItemId = source.OrderItemId,
                OrderId = orderId,
                //AmountGiven = source.AmountGiven,
                Discount = source.Discount,
                PurchasePrice = source.PurchasePrice,
                ProductVariationId = source.ProductId,//ProductId actually is ProductVariationId
                SalePrice = source.SalePrice,
                //MinSalePriceAllowed = source.MinSalePriceAllowed,
                Quantity = source.Quantity,

                //Comments = source.Comments,
                //RecCreatedBy = source.RecCreatedBy,
                //RecCreatedDate = source.RecCreatedDate,
                //RecLastUpdatedBy = source.RecLastUpdatedBy,
                //RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }
    }
}