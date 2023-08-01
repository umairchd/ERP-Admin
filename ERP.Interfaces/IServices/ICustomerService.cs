using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IServices
{
    public interface ICustomerService
    {
        Customer GetCustomer(long customerId);
        IEnumerable<Customer> GetAllCustomers();
        long AddCustomer(Customer customer);
        Customer GetCustomerByEmailOrPhone(string query);
    }
}
