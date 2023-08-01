using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IServices
{
    public interface IShelfService
    {
        IEnumerable<Shelf> GetAllShelves();
        Shelf GetShelf(long id);

        long AddShelf(Shelf shelf);
    }
}
