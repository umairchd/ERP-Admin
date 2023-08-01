using System.Collections.Generic;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
    public class OrderItemsService:IOrderItemService
    {
        private readonly IOrderItemsRepository orderItemsRepository;
        private readonly IProductVariationRepository productVariationRepository;

        public OrderItemsService(IOrderItemsRepository orderItemsRepository,IProductVariationRepository productVariationRepository)
        {
            this.orderItemsRepository = orderItemsRepository;
            this.productVariationRepository = productVariationRepository;
        }

        public IEnumerable<OrderItem> GetOrderItems(long orderId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<OrderItem> GetAllOrderItems()
        {
            throw new System.NotImplementedException();
        }


        public OrderItem GetOrderItemById(long orderItemId)
        {
            return orderItemsRepository.Find(orderItemId);
        }


        public long AddService(OrderItem orderItem)
        {
            orderItemsRepository.Add(orderItem);
            orderItemsRepository.SaveChanges();
            return orderItem.OrderItemId;
        }
        public bool UpdateService(OrderItem orderItem)
        {
           orderItemsRepository.Update(orderItem);
            orderItemsRepository.SaveChanges();
            return true;
        }

        public void AddUpdateService(Order order)
        {
            foreach (var item in order.OrderItems)
            {
                if (item.OrderItemId <= 0)
                    AddService(item);
                else
                    UpdateService(item);
            }
        }
        public OrderItem LoadOrderItemByProductVariationId(string productVariationId, long orderId)
        {
            var pv = productVariationRepository.GetProductByAnyCode(productVariationId);
            return orderItemsRepository.GetItemByProductVariationId(pv.ProductVariationId, orderId);
        }
    }
}
