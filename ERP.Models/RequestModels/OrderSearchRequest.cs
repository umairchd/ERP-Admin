using System;
using ERP.Models.Common;

namespace ERP.Models.RequestModels
{
    public class OrderSearchRequest : GetPagedListRequest
    {
        public string  OrderId { get; set; }
        public DateTime ? OrderFromDate { get; set; }
        public DateTime ? OrderToDate { get; set; }
        public string  ProductCode { get; set; }


        public OrdersByColumn OrdersOrderBy
        {
            get
            {
                return (OrdersByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
        public OrderSearchRequest()
        {
            SortBy = 0;
            IsAsc = false;
        }

    }
}
