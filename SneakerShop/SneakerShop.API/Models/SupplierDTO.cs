using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.Models
{
    public class SupplierDTO
    {
        public Guid SupplierId { get; set; }

        public string CompanyName { get; set; }
    }
}
