using System.Data.Entity;
using ERP.Interfaces.IRepository;
using ERP.Models.DomainModels;
using ERP.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace ERP.Repository.Repositories
{
    public sealed class PaymentMethodRepository : BaseRepository<PaymentMethod>, IPaymentMethodRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentMethodRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PaymentMethod> DbSet
        {
            get { return db.PaymentMethods; }
        }
    }
}
