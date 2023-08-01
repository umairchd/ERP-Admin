using System.Collections.Generic;

namespace ERP.Models.WebModels
{
    public class CategoryApiModel
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        public IEnumerable<SubCategoryApiModel> SubCategories { get; set; }
    }
}