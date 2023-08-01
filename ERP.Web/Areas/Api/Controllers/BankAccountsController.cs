using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ERP.Interfaces.IServices;
using ERP.Models.DropdownListModels;
using ERP.Models.ModelMapers;

namespace ERP.Web.Areas.Api.Controllers
{
    public class BankAccountsController : ApiController
    {
        private readonly ISupplierBankAccountService bankAccountService;

        public BankAccountsController(ISupplierBankAccountService bankAccountService)
        {
            this.bankAccountService = bankAccountService;
        }


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }
        public List<BankAccountsDdl> Get(long supplierId)
        {
            var ddl = bankAccountService.GetAllBankAccountsByVendorId(supplierId).Select(x => x.MapDdlFromServerToClient()).ToList();
            return ddl;
        }



        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}