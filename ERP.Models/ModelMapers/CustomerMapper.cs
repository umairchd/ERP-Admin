using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class CustomerMapper
    {
        public static CustomerModel CreateFromServerToClient(this Customer source)
        {
            return new CustomerModel
            {
               Id = source.Id,
               Name = source.Name,
               Phone = source.Phone,
               Address =  source.Address,
               Email = source.Email,

                Comments = source.Comments,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }
        public static Customer CreateFromClientToServer(this CustomerModel source)
        {
            return new Customer
            {
                Id = source.Id,
                Name = source.Name,
                Phone = source.Phone,
                Address = source.Address,
                Email = source.Email,

                Comments = source.Comments,

                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate
            };
        }
    }
}