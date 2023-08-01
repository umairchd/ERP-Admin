using System.Collections.Generic;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
    public class ShelfService:IShelfService
    {
        private readonly IShelfRepository shelfRepository;

        public ShelfService(IShelfRepository shelfRepository)
        {
            this.shelfRepository = shelfRepository;
        }

        public IEnumerable<Shelf> GetAllShelves()
        {
            return shelfRepository.GetAll();
        }

        public Shelf GetShelf(long id)
        {
            return shelfRepository.Find(id);
        }

        public long AddShelf(Shelf shelf)
        {
            if (shelf.ShelfId > 0)
                shelfRepository.Update(shelf);
            else
                shelfRepository.Add(shelf);

            shelfRepository.SaveChanges();
            return shelf.ShelfId;
        }
    }
}
