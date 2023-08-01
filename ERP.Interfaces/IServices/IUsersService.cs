using System;
using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IServices
{
     public interface  IUsersService  : IDisposable
     {

         IEnumerable<AspNetUser> GetAllUsers();
     }
}
