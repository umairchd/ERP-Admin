using System;
using System.Collections.Generic;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;
using ERP.Models.ModelMapers;
using ERP.Models.RequestModels;
using ERP.Models.ResponseModels;
using ERP.Models.WebModels;

namespace ERP.Service.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly IReturnedItemRepository returnedItemRepository;
        private readonly IOrderItemsRepository orderItemsRepository;


        public OrdersService(IOrdersRepository ordersRepository,IOrderItemsRepository orderItemsRepository, IReturnedItemRepository returnedItemRepository)
        {
            this.ordersRepository = ordersRepository;
            this.orderItemsRepository = orderItemsRepository;
            this.returnedItemRepository = returnedItemRepository;
        }


        public Order GetOrders(long orderId)
        {
            return ordersRepository.Find(orderId);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            throw new System.NotImplementedException();
        }


        public long AddService(Order order)
        {
            try
            {
                ordersRepository.Add(order);
                ordersRepository.SaveChanges();
                return order.OrderId;

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public long AddReturnItem(ReturnedOrderItem returnedOrderItem)
        {
            returnedItemRepository.Add(returnedOrderItem);
            returnedItemRepository.SaveChanges();
            return returnedOrderItem.ReturnId;
        }

        public bool UpdateService(Order order)
        {
            try
            {
                ordersRepository.Update(order);
                
                ordersRepository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw e;
                
            }
        }

        public bool DeleteOrder(long orderId)
        {
            var order = ordersRepository.Find(orderId);
            if (order != null)
            {
                order.IsDeleted = true;
                ordersRepository.Update(order);
                ordersRepository.SaveChanges();
                return true;
            }
            return false;
        }

        public ReturnedOrderItemModel GetReturnedOrderItem(long returnId)
        {
            var returnedItem = returnedItemRepository.GetReturnedOrderItem(returnId);

            if (returnedItem == null) return null;

            var orderItem = orderItemsRepository.GetItemByProductVariationId(returnedItem.ProductVariationId, returnedItem.OrderId);
            return returnedItem.CreateDetailesOrderItem(orderItem);
        }


        public OrderSearchResponse GetOrdersSearchResponse(OrderSearchRequest searchRequest)
        {
            return ordersRepository.GetOrdersSearchResponse(searchRequest);
        }

        public ReturnItemsSearchResponse GetReturnItemsSearchResponse(ReturnItemSearchRequest searchRequest)
        {
            return returnedItemRepository.GetReturnItemsSearchResponse(searchRequest);
        }
        
    }
}
