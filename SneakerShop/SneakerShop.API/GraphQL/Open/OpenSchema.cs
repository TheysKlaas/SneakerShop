using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Open
{
    public class OpenSchema : Schema
    {
        public OpenSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<OpenQuery>();
            Mutation = resolver.Resolve<OpenMutation>();            
        }
    }
}
