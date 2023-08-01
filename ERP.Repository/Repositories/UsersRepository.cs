﻿using System.Collections.Generic;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class UsersRepository : BaseRepository<AspNetUser>, IUsersRepository
    {
        #region Constructor
        public UsersRepository(IUnityContainer container)
            : base(container)
        {

        }
        protected override System.Data.Entity.IDbSet<AspNetUser> DbSet
        {
            get { return db.AspNetUsers; }
        }
        #endregion
        public IEnumerable<AspNetUser> GetAllUsers()
        {
            return DbSet.Where(x => x.AspNetRoles.Any(y => y.Name != "Admin"));//TEmperory check
        }
    }
}
