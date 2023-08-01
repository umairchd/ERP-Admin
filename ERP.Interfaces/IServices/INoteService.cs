using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IServices
{
    public interface INoteService
    {
        Note GetNote(long id);
        IEnumerable<Note> GetAllNotes();
        long AddNote(Note note);
    }
}
