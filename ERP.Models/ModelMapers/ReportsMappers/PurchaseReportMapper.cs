using ERP.Models.DomainModels;
using ERP.Models.ReportsModels;

namespace ERP.Models.ModelMapers.ReportsMappers
{
    public static class PurchaseReportMapper
    {
        public static PurchaseReport CreateReport(this PurchaseBillItem source)
        {
            PurchaseReport report = new PurchaseReport();

            //report.VendorId = source.VendorId;
            report.ProductCode = source.ProductVariationId;
            report.ProductName = source.ProductVariation.Product.Name;
            report.PurchasedQuantity = source.Quantity;
            //report.PurchasingDate = source.PurchasingDate;
            report.PurchasePrice = source.PurchasePrice;
            //report.VendorName = source.Vendor.Name;

            return report;
        }
    }
}
