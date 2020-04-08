using MongoDB.Driver;
using SneakerShop.NoSQLModels.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SneakerShop.NoSQLModels.Repositories
{
    public class OrderNoSQLRepo : IOrderNoSQLRepo
    {
        private readonly OrdersDBContext orderDBContext;
        public OrderNoSQLRepo(OrdersDBContext orderDBContext)
        {
            this.orderDBContext = orderDBContext;
        }

        //*** GET -------------------------------------------------------------
        public async Task<IEnumerable<OrderNoSQL>> GetAll()
        {
            try
            {
                //1. docs ophalen
                IMongoCollection<OrderNoSQL> collection =
                orderDBContext.Database.GetCollection<OrderNoSQL>("Orders");

                return await collection.Find(FilterDefinition<OrderNoSQL>.Empty)
                .ToListAsync<OrderNoSQL>();

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public async Task<OrderNoSQL> CreateAsync(OrderNoSQL o)
        {
            try
            {
                await orderDBContext.Orders.InsertOneAsync(o);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return o;
        }
    }
}
