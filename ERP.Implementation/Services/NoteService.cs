using System.Collections.Generic;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository notesRepository;

        public NoteService(INoteRepository notesRepository)
        {
            this.notesRepository = notesRepository;
        }

        public Note GetNote(long id)
        {
            return notesRepository.Find(id);
        }

        public IEnumerable<Note> GetAllNotes()
        {
            return notesRepository.GetAll();
        }        

        public long AddNote(Note note)
        {
            if (note.Id > 0)
                notesRepository.Update(note);
            else
                notesRepository.Add(note);

            notesRepository.SaveChanges();
            return note.Id;
        }
    }
}

