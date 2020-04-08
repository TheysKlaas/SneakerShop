using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Types.Input
{
    public class LoginInputType : InputObjectGraphType
    {
        public LoginInputType()
        {
            Name = "loginInput";
            Field<NonNullGraphType<StringGraphType>>("UserName");
            Field<NonNullGraphType<StringGraphType>>("Password");
        }
    }
}
