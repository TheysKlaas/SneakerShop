using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SneakerShop.NoSQLModels.Data
{
    public class OrdersDBContext
    {
        public IMongoDatabase Database;
        public OrdersDBContext(IMongoSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            Database = client.GetDatabase(settings.DatabaseName);
        }
        public IMongoCollection<OrderItemNoSQL> OrderItems =>
            Database.GetCollection<OrderItemNoSQL>("OrderItems");

        public IMongoCollection<OrderNoSQL> Orders =>
            Database.GetCollection<OrderNoSQL>("Orders");
        public async Task<bool> CollectionExistsAsync(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            //filter by collection name
            var collections = await Database.ListCollectionsAsync(
            new ListCollectionsOptions { Filter = filter });
            //check for existence
            return await collections.AnyAsync();
        }
    }
}
