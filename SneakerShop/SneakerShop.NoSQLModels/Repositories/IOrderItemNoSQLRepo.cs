using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.NoSQLModels.Repositories
{
    public interface IOrderItemNoSQLRepo
    {
        Task<IEnumerable<OrderItemNoSQL>> GetAll();
        Task<OrderItemNoSQL> CreateAsync(OrderItemNoSQL oi);
        Task<OrderItemNoSQL> Get(string orderId, Guid productId);
        //Task<ILookup<Guid, OrderItemNoSQL>> GetOrderItems(IEnumerable<Guid> orderIDGuids);
        Task<List<OrderItemNoSQL>> GetAllForOrderID(string id);
    }
}