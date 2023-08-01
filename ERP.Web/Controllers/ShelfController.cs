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
    public class ShelfController : Controller
    {
        private readonly IShelfService shelfService;

        public ShelfController(IShelfService shelfService)
        {
            this.shelfService = shelfService;
        }

        // GET: Brand
        public ActionResult Index()
        {
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            IEnumerable<ShelfModel> shelfs = shelfService.GetAllShelves().Select(x => x.CreateFromServerToClient());
            return View(shelfs);
           
        }

        
        public ActionResult Create(long? id)
        {
            ShelfModel model = new ShelfModel();
            if (id != null)
            {
                var shelf = shelfService.GetShelf((long)id);
                if (shelf != null)
                    model = shelf.CreateFromServerToClient();
            }
            return View(model);
        }

        //
        // POST: /Brand/Create
        [HttpPost]
        public ActionResult Create(ShelfModel shelf)
        {
          try
            {

                if (shelf.ShelfId == 0)
                {
                    shelf.RecCreatedBy = User.Identity.Name;
                    shelf.RecCreatedDate = DateTime.Now;
                }
               
                    shelf.RecLastUpdatedBy = User.Identity.Name;
                    shelf.RecLastUpdatedDate = DateTime.Now;
                    if (shelfService.AddShelf(shelf.CreateFromClientToServer()) > 0)
                    {
                        //Product Saved
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Shelf has been saved successfully.",
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