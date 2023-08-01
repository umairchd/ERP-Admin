namespace ERP.Models.DomainModels
{
    public class ProductCategoriesView
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public long MainCategoryId { get; set; }
        public string MainCategoryName { get; set; }
    }
}
