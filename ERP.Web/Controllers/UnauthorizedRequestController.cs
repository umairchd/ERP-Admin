using System.Web.Mvc;

namespace ERP.Web.Controllers
{
    public class UnauthorizedRequestController : Controller
    {
        //
        // GET: /UnauthorizedRequest/
        public ActionResult Index()
        {
            return View();
        }
	}
}