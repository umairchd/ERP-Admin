using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;

namespace ERP.Service.Services
{
    public class ReturnedItemService : IReturnedItemService
    {
        private readonly IReturnedItemRepository returnedItemsRepository;

        public ReturnedItemService(IReturnedItemRepository returnedItemsRepository)
        {
            this.returnedItemsRepository = returnedItemsRepository;
        }

    }
}
