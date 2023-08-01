using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class SupplierBankAccountRepository: BaseRepository<SupplierBankAccount> , ISupplierBankAccountRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SupplierBankAccountRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SupplierBankAccount> DbSet
        {
            get { return db.SupplierBankAccount; }
        }
        #endregion

        public IEnumerable<SupplierBankAccount> GetAllBankAccounts()
        {
            return DbSet.Include(x=>x.Supplier).Select(x => x).ToList();
        }
        public IEnumerable<SupplierBankAccount> GetAllBankAccountsByVendorId(long supllierId)
        {
            return DbSet.Where(x=>x.SupplierId == supllierId).Select(x => x).OrderBy(x=>x.IsActive).ToList();
        }
    }
}
