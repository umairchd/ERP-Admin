using System;
using ERP.Models.Common;

namespace ERP.Models.RequestModels
{
    public class ReturnItemSearchRequest : GetPagedListRequest
    {
        public ReturnItemSearchRequest()
        {
            SortBy = 0;
            IsAsc = false;
        }
        public string  OrderId { get; set; }
        public DateTime ? ReturnDateFrom { get; set; }
        public DateTime? ReturnDateTo { get; set; }
        public string  ProductCode { get; set; }


        public ReturnItemByColumn ReturnItemOrderBy
        {
            get
            {
                return (ReturnItemByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
        

    }
}
