using System.Collections.Generic;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
    public class NotesCategoryService : INotesCategoryService
    {
        private readonly INotesCategoryRepository notesCategoryRepository;

        public NotesCategoryService(INotesCategoryRepository notesCategoryRepository)
        {
            this.notesCategoryRepository = notesCategoryRepository;
        }

        public NotesCategory GetNotesCategory(long id)
        {
            return notesCategoryRepository.Find(id);
        }

        public IEnumerable<NotesCategory> GetAllNotesCategories()
        {
            return notesCategoryRepository.GetAll();
        }        

        public long AddNotesCategory(NotesCategory category)
        {
            if (category.Id > 0)
                notesCategoryRepository.Update(category);
            else
                notesCategoryRepository.Add(category);

            notesCategoryRepository.SaveChanges();
            return category.Id;
        }
    }
}

