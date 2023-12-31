﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public class AspNetUserRepository : BaseRepository<AspNetUser>, IAspNetUserRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AspNetUserRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<AspNetUser> DbSet
        {
            get { return db.AspNetUsers; }
        }

        #endregion

        public IEnumerable<AspNetUser> GetAllUsers()
        {

            var adminUsers = db.Users.Include("AspNetRoles").Where(t=>t.AspNetRoles.Any() && t.AspNetRoles.Any(x=>x.Name!="Admin"));
            
            var abc = adminUsers.ToList();
            return adminUsers;
            // return DbSet.Include("AspNetRoles").Where(x => x.Id != "53752d93-51ba-4374-86fe-c289a8662872");//AspNetRoles.Any(y => y.Name != "Admin")

            //.Where(x => x.AspNetRoles.Any(y => y.Name != "Admin"));//TEmperory check
        }

        public new IEnumerable<AspNetRole> Roles()
        {
            throw new System.NotImplementedException();
        }
    }
}
