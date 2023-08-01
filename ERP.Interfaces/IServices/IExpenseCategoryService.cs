using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IServices
{
    public interface IExpenseCategoryService
    {
        ExpenseCategory GetExpenseCategory(long expenseCategoryId);
        IEnumerable<ExpenseCategory> GetAllExpenseCategories();
        long AddExpenseCategory(ExpenseCategory expenseCategory);
    }
}
