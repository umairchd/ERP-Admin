using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class ExpenseCategoryMapper
    {
        public static ExpenseCategory CreateFromClientToServer(this ExpenseCategoryModel source)
        {
            return new ExpenseCategory
            {
                Id =source.Id,               
                Name = source.Name,                
                Description = source.Description,                
               
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }

        public static ExpenseCategoryModel CreateFromServerToClient(this ExpenseCategory source)
        {
            return new ExpenseCategoryModel
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }
    }
}