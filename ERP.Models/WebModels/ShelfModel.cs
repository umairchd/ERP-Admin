using System.ComponentModel.DataAnnotations;

namespace ERP.Models.WebModels
{
    public class ShelfModel
    {
        public long ShelfId { get; set; }
        [Display(Name = "Shelf Title")]
        public string Title { get; set; }
        [Display(Name = "Shelf Description")]
        public string Description { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
    }
}
