using MongoDB.Driver;
using SneakerShop.NoSQLModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerShop.NoSQLModels.Repositories
{
    public class OrderItemNoSQLRepo : IOrderItemNoSQLRepo
    {
        private readonly OrdersDBContext orderItemDBContext;
        public OrderItemNoSQLRepo(OrdersDBContext orderItemDBContext)
        {
            this.orderItemDBContext = orderItemDBContext;
        }

        //*** GET -------------------------------------------------------------
        public async Task<IEnumerable<OrderItemNoSQL>> GetAll()
        {
            try
            {
                //1. docs ophalen
                IMongoCollection<OrderItemNoSQL> collection =
                orderItemDBContext.Database.GetCollection<OrderItemNoSQL>("OrderItems");

                return await collection.Find(FilterDefinition<OrderItemNoSQL>.Empty)
                .ToListAsync<OrderItemNoSQL>();

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public async Task<OrderItemNoSQL> CreateAsync(OrderItemNoSQL oi)
        {
            await orderItemDBContext.OrderItems.InsertOneAsync(oi);
            return oi;
        }

        public async Task<OrderItemNoSQL> Get(string orderId, Guid productId)
        {
            var history = await orderItemDBContext.OrderItems
            .Find(oi => oi.OrderID == orderId && oi.ProductID == productId).FirstOrDefaultAsync<OrderItemNoSQL>(); //of findAsync()

            return history;
        }

        //public async Task<ILookup<Guid, OrderItemNoSQL>> GetOrderItems(IEnumerable<Guid> orderIDGuids)
        //{
        //    IMongoCollection<OrderItemNoSQL> collection = orderItemDBContext.Database.GetCollection<OrderItemNoSQL>("OrderItems");
        //    var orderItems = await collection.Find(
        //        oi => orderIDGuids.Contains(oi.OrderID)).ToListAsync();
        //    return orderItems.ToLookup(oi => oi.OrderID);
        //}



        public async Task<List<OrderItemNoSQL>> GetAllForOrderID(string id)
        {
            var orderItem = await orderItemDBContext.OrderItems
            .Find(oi => oi.OrderID == id).ToListAsync(); //of findAsync()

            return orderItem;
        }
    }
}
