using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IRepository
{
    public interface IExpenseRepository : IBaseRepository<Expense, long>
    {
        IEnumerable<Expense> GetExpenses(int year, int month, string vendor);
    }
}
