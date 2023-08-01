using ERP.Models.DomainModels;

namespace ERP.Interfaces.IRepository
{
    public interface IBrandRepository : IBaseRepository<Brand, long>
    {
        Brand GetBrandWithName(string brandName);




    }
}
