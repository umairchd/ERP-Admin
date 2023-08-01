using System.Collections.Generic;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
    public class BrandService:IBrandService
    {
        private readonly IBrandRepository brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            return brandRepository.GetAll();
        }

        public Brand GetBrand(long id)
        {
            return brandRepository.Find(id);
        }

        public long CheckBrandName(string brandName)
        {
            Brand b = brandRepository.GetBrandWithName(brandName);
            if (b == null)
                return 0;
            else
                return 1;            
        } 
     

        public long AddBrand(Brand brand)
        {
            if (brand.BrandId > 0)
                brandRepository.Update(brand);
            else
                brandRepository.Add(brand);

            brandRepository.SaveChanges();
            return brand.BrandId;
        }
    }
}
