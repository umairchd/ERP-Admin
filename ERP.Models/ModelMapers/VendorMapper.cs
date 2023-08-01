using ERP.Models.DomainModels;
using ERP.Models.DropdownListModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class VendorMapper
    {
        public static Supplier CreateFromClientToServer(this VendorModel source)
        {
            return new Supplier
            {
                SupplierId = source.VendorId,
                Name = source.Name,
                ActiveFlag = source.ActiveFlag,
                ContactNo = source.ContactNo,
                
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }

        public static VendorModel CreateFromServerToClient(this Supplier source)
        {
            return new VendorModel
            {
                VendorId = source.SupplierId,
                Name = source.Name,
                ActiveFlag = source.ActiveFlag,
                ContactNo = source.ContactNo,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }
        public static VendorsDdl MapVendorsDDL(this Supplier source)
        {
            return new VendorsDdl
            {
                VendorId = source.SupplierId,
                Name = source.Name
            };
        }
    }
}