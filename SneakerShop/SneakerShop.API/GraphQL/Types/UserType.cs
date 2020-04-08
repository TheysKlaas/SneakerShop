using GraphQL.Types;
using SneakerShop.Models;
using SneakerShop.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType(SneakerShopContext data)
        {
            Name = "User";

            Field(u => u.Id, type: typeof(IdGraphType)).Description("The id of the User.");
            Field(u => u.UserName, nullable: false).Description("The Username of the User.");
            Field(u => u.FirstName, nullable: false).Description("The Firstname of the User.");
            Field(u => u.LastName, nullable: false).Description("The Lastname of the User.");            
            Field(u => u.City, nullable: false).Description("The City of the User.");
            Field(u => u.Country, nullable: false).Description("The Country of the User.");
        }
    }
}
