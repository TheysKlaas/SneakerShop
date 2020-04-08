using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Types.Input
{
    public class OrderInputType : InputObjectGraphType
    {
        public OrderInputType()
        {
            Name = "orderInput";
            Field<NonNullGraphType<StringGraphType>>("UserID");
            Field<NonNullGraphType<DateTimeGraphType>>("Timestamp");
            Field<NonNullGraphType<DecimalGraphType>>("TotalPrice");
        }
    }
}
