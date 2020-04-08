//using SneakerShop.NoSQLModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SneakerShop.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; } = new Guid();
        public string UserID { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public decimal TotalPrice { get; set; }
        public ICollection<OrderItem> Products { get; set; }
        public virtual User User { get; set; }
    }
}
