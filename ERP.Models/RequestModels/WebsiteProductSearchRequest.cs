using ERP.Models.Common;

namespace ERP.Models.RequestModels
{
    public class WebsiteProductSearchRequest
    {
        public int PageSize { get; set; }
        private int _pageNo;
        public int PageNo
        {
            get
            {
                return _pageNo;
            }
            set
            {
                _pageNo = value == 0 ? 1 : value;
            }
        }

        //sort order
        public bool IsAsc { get; set; }

        // delete item id
        public int Id { get; set; }

        /// <summary>
        /// Order By Name
        /// </summary>

        public short SortBy { get; set; }

        public string Keyword { get; set; }

        public long MainCategoryId { get; set; }
        public long SubCategoryId { get; set; }

        public WebsiteProductByColumn ProductOrderBy
        {
            get
            {
                return (WebsiteProductByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
