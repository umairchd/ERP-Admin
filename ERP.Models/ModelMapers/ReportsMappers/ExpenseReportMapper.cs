using ERP.Models.DomainModels;
using ERP.Models.ReportsModels;

namespace ERP.Models.ModelMapers.ReportsMappers
{
    public static class ExpenseReportMapper
    {
        public static ExpenseReport CreateReport(this Expense source)
        {
            return new ExpenseReport
            {
                ExpenseDate = source.ExpenseDate,
                ExpenseAmount = source.ExpenseAmount,
                VendorName = source.Supplier != null ? source.Supplier.Name : string.Empty
            };
        }
    }
}
