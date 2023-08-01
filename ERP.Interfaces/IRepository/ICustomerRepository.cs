using ERP.Models.DomainModels;

namespace ERP.Interfaces.IRepository
{
    public interface ICustomerRepository : IBaseRepository<Customer, long>
    {
        Customer GetCustomerByEmailOrPhone(string query);
    }
}
