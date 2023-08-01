﻿using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class ExpenseMapper
    {
        public static Expense CreateFromClientToServer(this ExpenseModel source)
        {
            return new Expense
            {
                Id =source.Id,
                ExpenseAmount = source.ExpenseAmount,
                ExpenseCategoryId = source.ExpenseCategoryId,
                ExpenseDate = source.ExpenseDate,
                Description = source.Description,                
                VendorId = source.VendorId,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }

        public static ExpenseModel CreateFromServerToClient(this Expense source)
        {
            return new ExpenseModel
            {
                Id = source.Id,
                ExpenseAmount = source.ExpenseAmount,
                ExpenseCategoryId = source.ExpenseCategoryId,
                ExpenseDate = source.ExpenseDate,
                Description = source.Description,
                Category = source.ExpenseCategory.Name,
                VendorId = source.VendorId,
                VendorName = source.Supplier != null ? source.Supplier.Name : string.Empty,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }
    }
}