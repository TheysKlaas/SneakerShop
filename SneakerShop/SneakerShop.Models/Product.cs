using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SneakerShop.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; } = new Guid();
        [Required]
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
