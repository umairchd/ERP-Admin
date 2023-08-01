using System.Web.Mvc;
using ERP.Web.Controllers;

namespace IdentitySample.Controllers

{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("Login","Account");
        }
        
        public ActionResult OneColumn()
        {
            return View();
        }
        public ActionResult TwoColumnOne()
        {
            return View();
        }
        public ActionResult TwoColumnTwo()
        {
            return View();
        }
        public ActionResult ThreeColumn()
        {
            return View();
        }
    }
}
