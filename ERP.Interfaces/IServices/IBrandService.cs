using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IServices
{
    public interface IBrandService
    {
        IEnumerable<Brand> GetAllBrands();
        Brand GetBrand(long id);

        long AddBrand(Brand brand);

        long CheckBrandName(string brandName);
    }
}
