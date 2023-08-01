using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IRepository
{
    public interface IAspNetUserRepository : IBaseRepository<AspNetUser, string>
    {
        IEnumerable<AspNetUser> GetAllUsers();
    }

}
