using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class SupplierPaymentHistoryMapper
    {
        public static SupplierPaymentHistory CreateFromClientToServer(this SupplierPaymentHistoryModel source)
        {
            return new SupplierPaymentHistory
            {
                Amount = source.Amount,
                Description = source.Description,
                PaymentMethodId = source.PaymentMethodId,
                SupplierPaymentHistoryId = source.SupplierPaymentHistoryId,
                SupplierBankAccountId = source.SupplierBankAccountId,
                SupplierId = source.SupplierId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
            };
        }

        public static SupplierPaymentHistoryModel CreateFromServerToClient(this SupplierPaymentHistory source)
        {
            return new SupplierPaymentHistoryModel
            {
                Amount = source.Amount,
                Description = source.Description,
                PaymentMethodId = source.PaymentMethodId,
                SupplierPaymentHistoryId = source.SupplierPaymentHistoryId,
                SupplierBankAccountId = source.SupplierBankAccountId,
                SupplierId = source.SupplierId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
            };
        }
        public static SupplierPaymentHistoryWebModel CreateHistoryFromServerToClient(this SupplierPaymentHistory source)
        {
            var paymentHistory = new SupplierPaymentHistoryWebModel
            {
                Amount = source.Amount,
                PaymentDate = source.RecCreatedDate.ToString("dd-MMM-yy"),
                PaymentMethod = source.PaymentMethod.Title,
                Supplier = source.Supplier.Name,
                SupplierPaymentHistoryId = source.SupplierPaymentHistoryId
            };
            if (source.SupplierBankAccount != null)
            {
                paymentHistory.SupplierBankAccountNumber = source.SupplierBankAccount.AccountNo;
                paymentHistory.SupplierBankAccountTitle = source.SupplierBankAccount.AccountTitle;
            }
            return paymentHistory;

        }
    }
}