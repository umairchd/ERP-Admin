using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IRepository
{
    public interface IVendorRepository : IBaseRepository<Supplier, long>
    {
        IEnumerable<Supplier> GetActiveVendors();
    }
}
