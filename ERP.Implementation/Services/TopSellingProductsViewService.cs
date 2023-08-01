using System.Collections.Generic;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
    public class TopSellingProductsViewService : ITopSellingProductsViewService
    {
        private readonly ITopSellingProductsViewRepository topSellingProductsViewRepository;

        public TopSellingProductsViewService(ITopSellingProductsViewRepository topSellingProductsViewRepository)
        {
            this.topSellingProductsViewRepository = topSellingProductsViewRepository;
        }

        public IEnumerable<TopSellingProductsView> GetTopSellingProducts()
        {
            return topSellingProductsViewRepository.GetTopSellingProducts();
        }
    }
}
