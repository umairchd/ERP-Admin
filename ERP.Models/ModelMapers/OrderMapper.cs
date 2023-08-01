using System.Configuration;
using System.Linq;
using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class OrderMapper
    {
        public static OrderModel CreateFromServerToClient(this Order source)
        {
            return new OrderModel
            {
               IsModified = source.IsModified,
                Comments = source.Comments,
                OrderId =  source.OrderId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                //Customer = source.Customer!=null ? source.Customer.CreateFromServerToClient() : new CustomerModel(),
                CustomerIds = source.CustomerOrders.Select(x=>x.CustomerId.ToString()).ToArray(),
               CustomerOrderModel = source.CustomerOrders.Select(x => x.CreateFromServerToClient()).ToList(),
                OrderItems = source.OrderItems.Select(x=>x.CreateFromServerToClientWithProductDetails()).ToList()
            };
        }

        public static CustomerOrderModel CreateFromServerToClient(this CustomerOrder source)
        {
            return new CustomerOrderModel
            {
                CustomerId = source.CustomerId
            };
        }


        public static Order CreateFromClientToServer(this OrderModel source)
        {
            return new Order
            {
                IsModified = source.IsModified,
                Comments = source.Comments,
                OrderId = source.OrderId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                OrderItems = source.OrderItems.Select(x => x.CreateFromClientToServer(source.OrderId)).ToList()
            };
        }
    
    
    //FOR LIST VIEW

        public static OrderListViewModel CreateFromServerToLVClient(this Order source)
        {
            var hostURL = ConfigurationManager.AppSettings["HostURL"];//"http://" + HttpContext.Current.Request.Url.Host.ToLower() + "/";//
            var item = source.OrderItems.ToList();
            var userName = source.AspNetUser.FirstName + " " + source.AspNetUser.LastName;
            //var customer = source.Customer;
            return new OrderListViewModel
            {
                OrderId=source.OrderId,
                OrderDetailLink = @"<a title='Click to open order' href='" + hostURL + "Sale/New?id=" + source.OrderId + "'> " + source.OrderId + "</a>",
                RecCreatedBy = userName,
                RecCreatedDate = source.RecCreatedDate.ToString("dd-MMM-yy"),
                TotalDiscount = item.Sum(x=>x.Discount),
                SubTotal = double.Parse(item.Sum(x => x.SalePrice * x.Quantity).ToString()),
                TotalItems = item.Sum(x => x.Quantity),
                OrderPrintLink = @"<a title='Click to print order' target='_blank' href='" + hostURL + "Sale/Print/" + source.OrderId + "'><i class='fa fa-print'></i> Print</a>",
                NetTotal = decimal.ToDouble(item.Sum(x => x.SalePrice * x.Quantity) - item.Sum(x => x.Discount)),
            };
        }
    }
}