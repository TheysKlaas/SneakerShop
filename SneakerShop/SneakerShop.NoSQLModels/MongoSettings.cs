using System;
using System.Collections.Generic;
using System.Text;

namespace SneakerShop.NoSQLModels
{
    public class MongoSettings : IMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
