using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ERP.Interfaces.IServices;
using ERP.Models.ModelMapers;
using ERP.Models.WebModels;

namespace ERP.Web.Areas.Api.Controllers
{
    public class CategoryController : ApiController
    {
        private IProductMainCategoryService productMainCategoryService;
        public CategoryController(IProductMainCategoryService productMainCategoryService)
        {
            this.productMainCategoryService = productMainCategoryService;
        }

        // GET api/<controller>

        public IList<CategoryApiModel> Get()
        {
            var cat = productMainCategoryService.GetAllProductMainCategoriesWeb().ToList();

            return cat.Select(x => x.MapCategoryServerToClient()).ToList();
        }

        //public JsonResult Get()
        //{
        //    var cat = productMainCategoryService.GetAllProductMainCategoriesWeb().ToList();

        //    return Json(cat.Select(x => x.MapCategoryServerToClient()).ToList(), JsonRequestBehavior.AllowGet);
        //}

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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