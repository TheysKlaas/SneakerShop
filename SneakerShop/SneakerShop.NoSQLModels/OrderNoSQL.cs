using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SneakerShop.NoSQLModels
{
    public class OrderNoSQL
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; set; }
        [BsonElement("UserID")]
        public string UserID { get; set; }
        [BsonElement("Timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
