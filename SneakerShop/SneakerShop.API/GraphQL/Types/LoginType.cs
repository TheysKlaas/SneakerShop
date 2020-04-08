using GraphQL.Types;
using SneakerShop.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Types
{
    public class LoginType : ObjectGraphType<Login>
    {
        public LoginType()
        {
            Field(l => l.UserName, type: typeof(StringGraphType)).Description("The name of the user.");
            Field(l => l.Password, type: typeof(StringGraphType)).Description("The pw of the user.");
        }
    }
}
