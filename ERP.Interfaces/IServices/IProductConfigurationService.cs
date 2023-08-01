using ERP.Models.DomainModels;

namespace ERP.Interfaces.IServices
{
    public interface IProductConfigurationService
    {
        ProductConfiguration GetConfiguration(long configId);
        ProductConfiguration GetDefaultConfiguration();
        long SaveConfiguration(ProductConfiguration config);
    }
}
