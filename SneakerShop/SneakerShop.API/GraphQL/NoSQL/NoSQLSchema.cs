using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.NoSQL
{
    public class NoSQLSchema : Schema
    {
        public NoSQLSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<NoSQLQuery>();
        }
    }
}
