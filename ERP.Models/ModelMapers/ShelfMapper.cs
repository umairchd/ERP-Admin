using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class ShelfMapper
    {
        public static Shelf CreateFromClientToServer(this ShelfModel shelfModel)
        {
            return new Shelf()
            {
                ShelfId = shelfModel.ShelfId,
                Title = shelfModel.Title,
                Description = shelfModel.Description,
                RecCreatedBy = shelfModel.RecCreatedBy,
                RecCreatedDate = shelfModel.RecCreatedDate,
                RecLastUpdatedBy = shelfModel.RecLastUpdatedBy,
                RecLastUpdatedDate = shelfModel.RecLastUpdatedDate,
            };
        }
        public static ShelfModel CreateFromServerToClient(this Shelf shelf)
        {
            return new ShelfModel()
            {
                ShelfId = shelf.ShelfId,
                Title = shelf.Title,
                Description = shelf.Description,
                RecCreatedBy = shelf.RecCreatedBy,
                RecCreatedDate = shelf.RecCreatedDate,
                RecLastUpdatedBy = shelf.RecLastUpdatedBy,
                RecLastUpdatedDate = shelf.RecLastUpdatedDate,
            };
        }
    }
}