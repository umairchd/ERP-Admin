using System.Collections.Generic;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class NoteViewModel
    {
        public NoteViewModel()
        {
            NoteCategories = new List<NotesCategoryModel>();
        }
        public NoteModel NoteModel { get; set; }
        public IEnumerable<NotesCategoryModel> NoteCategories { get; set; }
    }
}