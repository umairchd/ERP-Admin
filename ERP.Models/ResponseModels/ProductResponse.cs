using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Models.ResponseModels
{
    public sealed class ProductResponse
    {
        public ProductResponse()
        {
            ProductCategories = new List<ProductCategory>();
        }

        /// <summary>
        /// Product Categories
        /// </summary>
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
        //ADDED BY UMER
        public IEnumerable<Brand> ProductBrands { get; set; }//
        public IEnumerable<Unit> Units { get; set; }//
        public IEnumerable<Shelf> Shelves { get; set; }//

        public Product Product { get; set; }
    }
}
