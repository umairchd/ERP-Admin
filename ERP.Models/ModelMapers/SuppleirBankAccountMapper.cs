using ERP.Models.DomainModels;
using ERP.Models.DropdownListModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class SuppleirBankAccountMapper
    {
        public static SupplierBankAccount CreateFromClientToServer(this SupplierBankAccountModel source)
        {
            return new SupplierBankAccount
            {
                SupplierBankAccountId = source.SupplierBankAccountId,
                SupplierId =source.SupplierId,
                Description = source.Description,             
                AccountTitle = source.AccountTitle,
                AccountNo = source.AccountNo,
                IsActive = source.IsActive,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }

        public static SupplierBankAccountModel CreateFromServerToClient(this SupplierBankAccount source)
        {
            return new SupplierBankAccountModel
            {
                SupplierBankAccountId = source.SupplierBankAccountId,
                SupplierId = source.SupplierId,
                Description = source.Description,
                AccountTitle = source.AccountTitle,
                AccountNo = source.AccountNo,
                IsActive = source.IsActive,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }
        public static SupplierBankAccountListModel CreateListFromServerToClient(this SupplierBankAccount source)
        {
            return new SupplierBankAccountListModel
            {
                SupplierBankAccountId = source.SupplierBankAccountId,
                Supplier = source.Supplier.Name,
                Description = source.Description,
                AccountTitle = source.AccountTitle,
                AccountNo = source.AccountNo,
                IsActive = source.IsActive,
                RecCreatedDate = source.RecCreatedDate,
            };
        }
        
        public static BankAccountsDdl MapDdlFromServerToClient(this SupplierBankAccount source)
        {
            return new BankAccountsDdl
            {
                SupplierBankAccountId = source.SupplierBankAccountId,
                AccountTitle = source.AccountTitle
            };
        }
    }
}