using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Restricted
{
    public class RestrictedSchema : Schema
    {
        public RestrictedSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RestrictedQuery>();
            Mutation = resolver.Resolve<RestrictedMutation>();
        }
    }
}
