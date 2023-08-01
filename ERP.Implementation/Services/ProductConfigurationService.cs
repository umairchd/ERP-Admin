using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
    public class ProductConfigurationService:IProductConfigurationService
    {
        private readonly IProductConfigurationRepositoy productConfigurationRepositoy;

        public ProductConfigurationService(IProductConfigurationRepositoy productConfigurationRepositoy)
        {
            this.productConfigurationRepositoy = productConfigurationRepositoy;
        }

        public ProductConfiguration GetConfiguration(long configId)
        {
            return productConfigurationRepositoy.Find(configId);
        }

        public ProductConfiguration GetDefaultConfiguration()
        {
            return productConfigurationRepositoy.GetDefaultConfiguration();
        }

        public long SaveConfiguration(ProductConfiguration config)
        {
            if(config.Id>0)
                productConfigurationRepositoy.Update(config);
            else
                productConfigurationRepositoy.Add(config);
            productConfigurationRepositoy.SaveChanges();
            return config.Id;
        }
    }
}
