using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IServices
{
    public interface IExpenseService
    {
        Expense GetExpense(long expenseCategoryId);
        IEnumerable<Expense> GetAllExpenses();
        long AddExpense(Expense expense);
    }
}
