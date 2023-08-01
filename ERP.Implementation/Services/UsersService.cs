using System.Collections.Generic;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
     public   class UsersService : IUsersService
    {

          #region 'Private and Constructor'
        private readonly IUsersRepository usersRepository;


        public UsersService(IUsersRepository iUsersRepository)
        {
            this.usersRepository = iUsersRepository;
            
        }

       #endregion 'Private and Constructor'


        public IEnumerable<AspNetUser> GetAllUsers()
        {
            return usersRepository.GetAllUsers();
        }

        public void Dispose()
        {
            
        }
    }
}
