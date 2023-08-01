using System.Linq;
using ERP.Models.WebModels;
using PurchaseBill = ERP.Models.DomainModels.PurchaseBill;

namespace ERP.Models.ModelMapers
{
    public static class PurchaseBillMapper
    {
        public static PurchaseBill CreateFromClientToServer(this WebModels.PurchaseBill source)
        {
            var bill = new PurchaseBill
            {
                PurchaseBillId = source.PurchaseBillId,
                PurchaseDate = source.PurchaseDate,
                VendorId = source.VendorId,
                VendorInvoiceNo = source.VendorInvoiceNo,
                TotalDiscount = source.TotalDiscount,
                PaidAmount = source.PaidAmount,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };

            return bill;
        }
        public static WebModels.PurchaseBill MapServerToClient(this PurchaseBill source)
        {
            var bill = new WebModels.PurchaseBill
            {
                PurchaseBillId = source.PurchaseBillId,
                PurchaseDate = source.PurchaseDate,
                VendorId = source.VendorId,
                VendorInvoiceNo = source.VendorInvoiceNo,
                TotalDiscount = source.TotalDiscount,
                PaidAmount = source.PaidAmount,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };

            return bill;
        }

        public static PurchaseBillHistory MapBillHistoryFromServerToClient(this PurchaseBill source)
        {
            return new PurchaseBillHistory
            {
                BillId = source.PurchaseBillId,
                GrossTotal = source.PurchaseBillItems.Sum(x=>x.PurchasePrice),
                Payment = source.PaidAmount,
                PurchaseDate = source.PurchaseDate.ToString("dd-MMM-yy"),
                SupplierInvoiceNumber = source.VendorInvoiceNo ?? "N/A",
                SupplierName = source.Supplier.Name
            };
        }
    }
}