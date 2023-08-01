using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IRepository
{
    public interface IUsersRepository : IBaseRepository<AspNetUser,int>
    {
        IEnumerable<AspNetUser> GetAllUsers();
    }
}
