using System.ComponentModel.DataAnnotations;

namespace ERP.Models.WebModels
{
    public class UnitModel
    {
        public int UnitId { get; set; }
        [Display(Name = "Unit Title")]
        public string UnitTitle { get; set; }
        [Display(Name = "Unit Value")]
        public int UnitValue { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecLastUpdatedBy { get; set; }
    }
}
