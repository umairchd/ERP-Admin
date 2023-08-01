using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IRepository
{
    public interface ITopSellingProductsViewRepository : IBaseRepository<TopSellingProductsView, long>
    {
        IEnumerable<TopSellingProductsView> GetTopSellingProducts();
    }
}
