using System.Collections.Generic;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            this.expenseRepository = expenseRepository;
        }

        public Expense GetExpense(long expenseId)
        {
            return expenseRepository.Find(expenseId);
        }

        public IEnumerable<Expense> GetAllExpenses()
        {
            return expenseRepository.GetAll();
        }

        public long AddExpense(Expense expense)
        {
            if (expense.Id > 0)
                expenseRepository.Update(expense);
            else
                expenseRepository.Add(expense);

            expenseRepository.SaveChanges();
            return expense.Id;
        }
    }
}

