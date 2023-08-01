using System.Web.Http;

namespace ERP.WebApi.Api
{
    public class ValueController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new { Data = "jadhkawh"});
        }
    }
}
