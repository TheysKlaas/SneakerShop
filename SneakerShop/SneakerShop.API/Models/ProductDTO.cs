using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.Models
{
    public class ProductDTO
    {
        //public Guid ProductId { get; set; } = new Guid();
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Supplier { get; set; }
    }
}
