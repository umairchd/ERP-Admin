using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IServices
{
    public interface INotesCategoryService
    {
        NotesCategory GetNotesCategory(long id);
        IEnumerable<NotesCategory> GetAllNotesCategories();
        long AddNotesCategory(NotesCategory notesCategory);
    }
}
