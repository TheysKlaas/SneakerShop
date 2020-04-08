using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SneakerShop.Models
{
    public class OrderItem
    {
        [Display(Name = "Order")]
        public Guid OrderID { get; set; }
        [Display(Name = "Product")]
        public Guid ProductID { get; set; }
        public int Size { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
