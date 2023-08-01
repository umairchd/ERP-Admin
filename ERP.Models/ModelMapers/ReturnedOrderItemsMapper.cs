using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class ReturnedItemsMapper
    {
        public static ReturnedOrderItem CreateFromClientToServer(this ReturnedOrderItemModel source)
        {
            return new ReturnedOrderItem
            {
               ReturnId = source.ReturnId,
               OrderId = source.OrderId,
               ProductVariationId = source.ProductVariationId,
               ReturnedQty = source.ReturnedQty,
               Price = source.Price,
               ReceivedBy = source.ReceivedBy,
               ReceivedDate = source.ReceivedDate
            };
        }
        public static ReturnedOrderItemModel CreateFromServerToClient(this ReturnedOrderItem source)
        {
            var item= new ReturnedOrderItemModel
            {
                ReturnId = source.ReturnId,
                OrderId = source.OrderId,
                ProductVariationId = source.ProductVariationId,
                Price = source.Price,
                ReturnedQty = source.ReturnedQty,
                ReceivedBy = source.ReceivedBy,
                ReceivedDate = source.ReceivedDate
            };
            return item;
        }
        public static ReturnedOrderItemModel CreateDetailesOrderItem(this ReturnedOrderItem source, OrderItem orderItem)
        {
            var item = new ReturnedOrderItemModel
            {
                ReturnId = source.ReturnId,
                OrderId = source.OrderId,
                ProductVariationId = source.ProductVariationId,
                Price = source.Price,
                ReturnedQty = source.ReturnedQty,
                ReceivedBy = source.ReceivedBy,
                ReceivedDate = source.ReceivedDate,
                ReturnedSubTotal = source.Price*source.ReturnedQty,
                SaleQty = orderItem.Quantity,
                SalePrice = orderItem.SalePrice,
                SaleDiscount = orderItem.Discount,
                SaleSubTotal = (orderItem.Quantity*orderItem.SalePrice) - orderItem.Discount
            };
            item.Deduction = ((item.SaleSubTotal - item.ReturnedSubTotal)/item.SaleSubTotal)*100;
            return item;
        }
        
        public static ReturnItemLVModel CreateLVFromServerToClient(this ReturnedOrderItem source)
        {
            return new ReturnItemLVModel()
            {
                ReturnId = source.ReturnId,
                OrderId = source.OrderId,
                ItemCode = source.ProductVariationId,
                Price = source.Price,
                ReturnedQty = source.ReturnedQty,
                ReceivedBy = source.AspNetUser.FirstName + " " + source.AspNetUser.LastName,
                ReturnedDate = source.ReceivedDate.ToShortDateString(),
                NetTotal = source.ReturnedQty*source.Price
            };
        }
    }
}