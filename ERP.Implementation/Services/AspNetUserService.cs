﻿using System.Collections.Generic;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
    public class AspNetUserService : IAspNetUserService
    {
        private readonly IAspNetUserRepository repository;
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xRepository"></param>
        public AspNetUserService(IAspNetUserRepository xRepository)
        {
            repository = xRepository;
        }

        #endregion
        public AspNetUser FindById(string id)
        {
            return repository.Find(id);
        }

        public IEnumerable<AspNetUser> GetAllUsers()
        {
            return repository.GetAllUsers();
        }
        public bool UpdateUser(AspNetUser user)
        {
            repository.Update(user);
            repository.SaveChanges();
            return true;
        }
    }
}
