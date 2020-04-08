using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SneakerShop.Models
{
    public class Supplier
    {
        [Key]
        public Guid SupplierId { get; set; } = new Guid();
        [Required]
        public string CompanyName { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
