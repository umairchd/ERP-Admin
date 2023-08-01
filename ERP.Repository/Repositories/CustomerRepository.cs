using System.Data.Entity;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class CustomerRepository: BaseRepository<Customer>, ICustomerRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Customer> DbSet
        {
            get { return db.Customers; }
        }
        #endregion

        public Customer GetCustomerByEmailOrPhone(string query)
        {
            return DbSet.FirstOrDefault(x => x.Email == query || x.Phone == query);
        }
    }
}
