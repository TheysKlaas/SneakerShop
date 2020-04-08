using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Types.Input
{
    public class ProductInputType : InputObjectGraphType
    {
        public ProductInputType()
        {
            Name = "productInput";
            Field<NonNullGraphType<IdGraphType>>("ProductId");
            Field<NonNullGraphType<StringGraphType>>("ProductName");
            Field<NonNullGraphType<DecimalGraphType>>("UnitPrice");
            Field<NonNullGraphType<IdGraphType>>("SupplierId");
    }
    }
}
