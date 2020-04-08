using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.Models.Repositories
{
    public interface IOrderRepo : IGenericRepo<Order>
    {
        new Task<IEnumerable<Order>> GetAllAsync();
        Order GetOrder(Guid OrderId);
        IEnumerable<Order> GetOrderForUser(string userId);
        new Task<Order> Create(Order o);
        Task<OrderItem> CreateOrderItem(OrderItem oi);
        Task<ILookup<Guid, OrderItem>> GetOrderItems(IEnumerable<Guid> orderIDGuids);
        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
    }
}