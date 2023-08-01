using System.Collections.Generic;
using ERP.Models.WebModels;

namespace ERP.Models.WebViewModels
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            ProductList = new List<ProductWebApiModel>();
        }
        public List<ProductWebApiModel> ProductList { get; set; }
        /// <summary>
        /// Message for cart
        /// </summary>
        public string Message { get; set; }

        public int TotalProducts { get; set; }

        /// <summary>
        /// message type in toastr
        /// </summary>
        public string MessageType { get; set; }

        public decimal SubTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Discount { get; set; }


    }
}
