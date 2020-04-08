using GraphQL.Authorization;
using GraphQL.DataLoader;
using GraphQL.Types;
using SneakerShop.Models;
using SneakerShop.Models.Data;
using SneakerShop.Models.Repositories;
using SneakerShop.NoSQLModels;
using SneakerShop.NoSQLModels.Repositories;
//using SneakerShop.NoSQLModels;
//using SneakerShop.NoSQLModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Types.NoSQLTypes
{
    public class NoSQLOrderType : ObjectGraphType<OrderNoSQL>
    {
        public NoSQLOrderType(IOrderItemNoSQLRepo orderRepo, IUserRepo userRepo)
        {

            Field(o => o.OrderId).Description("The id of the Order.");
            Field(o => o.UserID).Description("The id of the User.");
            Field(o => o.Timestamp).Description("The datetime of the Order.");
            Field(
                name: "User",
                type: typeof(UserType),
                resolve: context => userRepo.GetUserById(context.Source.UserID)
            );
            Field(
                name: "Products",
                type: typeof(ListGraphType<NoSQLOrderItemType>),
                resolve : context => orderRepo.GetAllForOrderID(context.Source.OrderId).GetAwaiter().GetResult()
                );
        }
    }
}
