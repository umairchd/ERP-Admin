using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Models.ResponseModels
{

    public sealed class ProductVariationSearchResponse
    {
        public ProductVariationSearchResponse()
        {
            ProductVariations = new List<ProductVariation>();
        }

        /// <summary>
        /// Products
        /// </summary>
        public IEnumerable<ProductVariation> ProductVariations { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>

        public int TotalCount { get; set; }

        public int FilteredCount { get; set; }

    }
}
