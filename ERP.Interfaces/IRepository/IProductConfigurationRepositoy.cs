using ERP.Models.DomainModels;

namespace ERP.Interfaces.IRepository
{
    public interface IProductConfigurationRepositoy : IBaseRepository<ProductConfiguration, long>
    {
        ProductConfiguration GetDefaultConfiguration();
    }
}
