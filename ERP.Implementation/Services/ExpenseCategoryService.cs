using System.Collections.Generic;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly IExpenseCategoryRepository expenseCategoryRepository;

        public ExpenseCategoryService(IExpenseCategoryRepository expenseCategoryRepository)
        {
            this.expenseCategoryRepository = expenseCategoryRepository;
        }

        public ExpenseCategory GetExpenseCategory(long expenseCategoryId)
        {
            return expenseCategoryRepository.Find(expenseCategoryId);
        }

        public IEnumerable<ExpenseCategory> GetAllExpenseCategories()
        {
            return expenseCategoryRepository.GetAll();
        }

        public long AddExpenseCategory(ExpenseCategory expenseCategory)
        {
            if (expenseCategory.Id > 0)
                expenseCategoryRepository.Update(expenseCategory);
            else
                expenseCategoryRepository.Add(expenseCategory);

            expenseCategoryRepository.SaveChanges();
            return expenseCategory.Id;
        }
    }
}

