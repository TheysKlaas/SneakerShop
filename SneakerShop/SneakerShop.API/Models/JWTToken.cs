using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.Models
{
    public class JWTToken
    {
        public string token { get; set; }
        public string expiration { get; set; }
        public string role { get; set; }
        public string currentUser { get; set; }
        public string phoneNumber { get; set; }
    }
}
