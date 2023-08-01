using System.Collections.Generic;
using System.Web.Http;
using ERP.Interfaces.IServices;
using ERP.Models.ModelMapers;
using ERP.Models.WebModels;

namespace ERP.Web.Areas.Api.Controllers
{
    public class OrderItemController : ApiController
    {
        private readonly IOrderItemService orderItemService;

        public OrderItemController(IOrderItemService orderItemService )
        {
            this.orderItemService = orderItemService;
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public OrderItemForApi Get(string itemId, long orderId)
        {
            var oi= orderItemService.LoadOrderItemByProductVariationId(itemId,orderId);
            return oi != null ? oi.CreatePureOrderItemForAPI() : null;
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