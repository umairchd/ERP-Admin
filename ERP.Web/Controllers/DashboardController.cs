using System.Web.Mvc;
using ERP.Interfaces.IServices;
using ERP.Models.ResponseModels;

namespace ERP.Web.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {        
        private readonly IDashboardService dashboardService;
        
        public DashboardController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }
        
        public ActionResult Index()
        {
            DashboardResponseModel model= dashboardService.GetDashboardData();
            return View(model);
        }

    }
}