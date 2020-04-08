using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Admin
{
    public class AdminSchema : Schema
    {
        public AdminSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<AdminQuery>();
            Mutation = resolver.Resolve<AdminMutation>();
        }
    }
}
