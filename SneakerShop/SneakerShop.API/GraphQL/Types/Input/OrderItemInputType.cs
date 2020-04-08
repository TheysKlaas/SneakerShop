using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Types.Input
{
    public class OrderItemInputType : InputObjectGraphType
    {
        public OrderItemInputType()
        {
            Name = "orderItemInput";
            Field<NonNullGraphType<IdGraphType>>("OrderID");
            Field<NonNullGraphType<IdGraphType>>("ProductID");
            Field<NonNullGraphType<IntGraphType>>("Size");
        }
    }
}
