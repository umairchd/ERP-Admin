using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class BrandMapper
    {
        public static Brand CreateFromClientToServer(this BrandWebModel brandWebModel)
        {
            return new Brand()
            {
                BrandId = brandWebModel.BrandId,
                BrandName = brandWebModel.BrandName,
                BrandDescription = brandWebModel.BrandDescription,
                RecCreatedBy = brandWebModel.RecCreatedBy,
                RecCreatedDate = brandWebModel.RecCreatedDate,
                RecLastUpdatedBy = brandWebModel.RecLastUpdatedBy,
                RecLastUpdatedDate = brandWebModel.RecLastUpdatedDate,
            };
        }
        public static BrandWebModel CreateFromServerToClient(this Brand brandDomainModel)
        {
            return new BrandWebModel()
            {
                BrandId = brandDomainModel.BrandId,
                BrandName = brandDomainModel.BrandName,
                BrandDescription = brandDomainModel.BrandDescription,
                RecCreatedBy = brandDomainModel.RecCreatedBy,
                RecCreatedDate = brandDomainModel.RecCreatedDate,
                RecLastUpdatedBy = brandDomainModel.RecLastUpdatedBy,
                RecLastUpdatedDate = brandDomainModel.RecLastUpdatedDate,
            };
        }
    }
}