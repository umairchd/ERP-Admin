using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class ExpenseRepository: BaseRepository<Expense>, IExpenseRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ExpenseRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Expense> DbSet
        {
            get { return db.Expenses; }
        }
        #endregion

        public IEnumerable<Expense> GetExpenses(int year, int month, string vendor)
        {
            IEnumerable<Expense> expenses = DbSet.Where(x => 
                DbFunctions.TruncateTime(x.ExpenseDate).Value.Year == year & 
                DbFunctions.TruncateTime(x.ExpenseDate).Value.Month == month &
                ((x.Supplier != null && x.Supplier.Name.Contains(vendor)) | x.Supplier == null)).ToList();
            return expenses;
        }
    }
}
