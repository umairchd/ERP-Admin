using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IServices
{
    public interface IVendorService
    {
        Supplier GetVendor(long vendorId);
        IEnumerable<Supplier> GetAllVendors();
        IEnumerable<Supplier> GetActiveVendors();
        long AddVendor(Supplier vendor);
    }
}
