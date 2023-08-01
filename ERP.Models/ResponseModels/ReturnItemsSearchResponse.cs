using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Models.ResponseModels
{
    public sealed class ReturnItemsSearchResponse
    {
        public ReturnItemsSearchResponse()
        {
            ReturnedOrderItems = new List<ReturnedOrderItem>();
        }
        public IEnumerable<ReturnedOrderItem> ReturnedOrderItems { get; set; }

        public int TotalCount { get; set; }

        public int FilteredCount { get; set; }
    }
}
