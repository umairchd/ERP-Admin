using System;
using ERP.Models.DomainModels;
using ERP.Models.ReportsModels;

namespace ERP.Models.ModelMapers.ReportsMappers
{
    public static class SalesReportMapper
    {
        public static SalesReport CreateReport(this OrderItem source)
        {
            SalesReport salesReport = new SalesReport();
            
            var itemPurchasePrice = source.ProductVariation.Purchaseprice;

            salesReport.Id = source.OrderItemId;
            //salesReport.Date = source.RecCreatedDate;
            salesReport.ProductCode = source.ProductVariationId;
            salesReport.ProductName = source.ProductVariation.Product.Name;
            salesReport.Quantity = source.Quantity;
            salesReport.SalePrice = source.SalePrice;
            salesReport.SubTotalSale = source.Quantity * source.SalePrice;
            //salesReport.Discount = source.Discount;
            salesReport.TotalSale = (source.Quantity * source.SalePrice) - source.Discount;
            salesReport.PurchasePrice = itemPurchasePrice;
            salesReport.TotalCost = source.Quantity * itemPurchasePrice;
            salesReport.TotalProfit = salesReport.TotalSale - salesReport.TotalCost;
            if (salesReport.TotalProfit > 0)
            {
                salesReport.ProfitPercentage = Math.Round(((salesReport.TotalProfit / salesReport.TotalCost) * 100), 2);    
            }
            else
            {
                salesReport.ProfitPercentage = 0;
            }
            
            salesReport.OrderId = source.OrderId;

            return salesReport;
        }
    }
}
