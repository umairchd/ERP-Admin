using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IServices
{
    public interface ITopSellingProductsViewService
    {
        IEnumerable<TopSellingProductsView> GetTopSellingProducts();
    }
}
