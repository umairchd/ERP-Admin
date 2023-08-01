using System.Collections.Generic;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class ExpenseViewModel
    {
        public ExpenseViewModel()
        {
            ExpenseCategories = new List<ExpenseCategoryModel>();
        }
        public ExpenseModel ExpenseModel { get; set; }
        public IEnumerable<ExpenseCategoryModel> ExpenseCategories { get; set; }
        public IEnumerable<VendorModel> Vendors { get; set; }
    }
}