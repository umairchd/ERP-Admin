using ERP.Models.DomainModels;
using ERP.Models.ReportsModels;

namespace ERP.Models.ModelMapers.ReportsMappers
{
    public static class ProductsStockMapper
    {
        public static StockReport CreateReport(this ProductsStock source)
        {
            return new StockReport
            {
               ProductCode = source.ProductId,
               ProductName = source.Name,
               AvailableQuantity = source.AvailableQty,
               PurchasedQuantity = source.PurchasedQty ?? 0,
               SalePrice = source.SalePrice,
               PurchasedPrice = source.PurchasePrice//Opposite just for the report
            };
        }       
    }
}