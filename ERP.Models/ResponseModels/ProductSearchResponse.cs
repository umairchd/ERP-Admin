using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Models.ResponseModels
{
    public sealed class ProductSearchResponse
    {
        public ProductSearchResponse()
        {
            Products = new List<Product>();
        }

        /// <summary>
        /// Products
        /// </summary>
        public IEnumerable<Product> Products { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>

        public int TotalCount { get; set; }

        public int FilteredCount { get; set; }

    }
}
