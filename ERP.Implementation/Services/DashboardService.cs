using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.ResponseModels;

namespace ERP.Service.Services
{
    public class DashboardService:IDashboardService
    {
        private readonly IProductVariationRepository productVariationRepository;
        private readonly IOrderItemsRepository orderItemsRepository;
        private readonly IProductRepository productRepository;

        public DashboardService(IProductVariationRepository productVariationRepository,IOrderItemsRepository orderItemsRepository,IProductRepository productRepository)
        {
            this.productVariationRepository = productVariationRepository;
            this.orderItemsRepository = orderItemsRepository;
            this.productRepository = productRepository;
        }

        public DashboardResponseModel GetDashboardData()
        {
            var result = productVariationRepository.GetShortOnStockProducts();
            //orderItemsRepository.GetDashboardWidgetsData(DateTime.Now);
            return new DashboardResponseModel()
            {
                TodayProfit = 10,
                TodayTopSoldProductId = 286,
                TodayTopSoldProductName = "MAC BOOK",
                TodayTotalSale = 20,
                ShortStockProducts = 15,

            };
        }
    }
}
