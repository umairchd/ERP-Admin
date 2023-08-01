using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Models.ResponseModels
{
    public sealed class OrderSearchResponse
    {
        public OrderSearchResponse()
        {
            Orders = new List<Order>();
        }

        ///// <summary>
        ///// Product Categories
        ///// </summary>
        //public IEnumerable<ProductCategory> ProductCategories { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public int TotalCount { get; set; }

        public int FilteredCount { get; set; }
    }
}
