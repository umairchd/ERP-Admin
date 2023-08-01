using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ERP.Interfaces.IServices;
using ERP.Models.ModelMapers;
using ERP.Models.WebModels;
using ERP.Models.WebViewModels;

namespace ERP.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UnitController : Controller
    {
        private readonly IUnitService unitService;

        public UnitController(IUnitService unitService)
        {
            this.unitService = unitService;
        }

        // GET: Brand
        public ActionResult Index()
        {
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            IEnumerable<UnitModel> units = unitService.GetAllUnits().Select(x => x.CreateFromServerToClient());
            return View(units);
           
        }

        
        public ActionResult Create(long? id)
        {
            UnitModel model = new UnitModel();
            if (id != null)
            {
                var unit = unitService.GetUnit((long)id);
                if (unit != null)
                    model = unit.CreateFromServerToClient();
            }
            return View(model);
        }

        //
        // POST: /Unit/Create
        [HttpPost]
        public ActionResult Create(UnitModel unit)
        {
            try
            {

                if (unit.UnitId == 0)
                {
                    unit.RecCreatedBy = User.Identity.Name;
                    unit.RecCreatedDate = DateTime.Now;
                }
                    unit.RecLastUpdatedBy = User.Identity.Name;
                    unit.RecLastUpdatedDate = DateTime.Now;
                if (unitService.AddUnit(unit.CreateFromClientToServer()) > 0)
                {
                    //Unit Saved
                    TempData["message"] = new MessageViewModel
                    {
                        Message = "Unit has been saved successfully.",
                        IsSaved = true
                    };
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}