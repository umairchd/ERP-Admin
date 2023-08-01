using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IRepository
{
    public interface IProductsStockRepository : IBaseRepository<ProductsStock, long>
    {
        IEnumerable<ProductsStock> StocksReport(string barCode, string productCode, string productName);
    }
}
