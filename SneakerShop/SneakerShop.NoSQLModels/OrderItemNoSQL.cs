using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SneakerShop.NoSQLModels
{
    public class OrderItemNoSQL
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderItemID { get; set; }
        [BsonElement("OrderID")]
        public string OrderID { get; set; }
        [BsonElement("ProductID")]
        public Guid ProductID { get; set; }
        [BsonElement("Size")]
        public int Size { get; set; }
    }
}
