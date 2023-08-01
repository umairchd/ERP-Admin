using System.Collections.Generic;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository vendorRepository;

        public VendorService(IVendorRepository vendorRepository)
        {
            this.vendorRepository = vendorRepository;
        }

        public Supplier GetVendor(long vendorId)
        {
            return vendorRepository.Find(vendorId);
        }

        public IEnumerable<Supplier> GetAllVendors()
        {
            return vendorRepository.GetAll().ToList();
        }

        public IEnumerable<Supplier> GetActiveVendors()
        {
            return vendorRepository.GetActiveVendors();
        }

        public long AddVendor(Supplier vendor)
        {
            if (vendor.SupplierId > 0)
                vendorRepository.Update(vendor);
            else
                vendorRepository.Add(vendor);

            vendorRepository.SaveChanges();
            return vendor.SupplierId;
        }
    }
}

