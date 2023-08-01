using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TMD.Interfaces.IRepository;
using TMD.Models.ModelMapers;
using TMD.Models.WebModels;

namespace TMD.Web.Areas.Api.Controllers
{
    public class ProductWebController : ApiController
    {
        private IProductRepository productRepository;

        public ProductWebController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public List<ProductWebApiModel> Get(long id)
        {
            var asd = productRepository.GetProductsById(id).ToList();
            return asd.Select(x => x.MapProductServerToClient()).ToList();
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