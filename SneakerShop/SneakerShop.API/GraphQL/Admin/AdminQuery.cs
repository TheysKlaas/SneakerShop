using GraphQL.Types;
using SneakerShop.API.GraphQL.Types;
using SneakerShop.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShop.API.GraphQL.Admin
{
    public class AdminQuery : ObjectGraphType
    {
        public AdminQuery(IOrderRepo orderRepo, IUserRepo userRepo)
        {
            // All Orders
            Field<ListGraphType<OrderType>>(
              "orders",
              resolve: context => orderRepo.GetAllAsync()
            );

            // Order by Id
            Field<OrderType>
                ("order",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    try
                    {
                        return orderRepo.GetOrder(id);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            );
            // users
            Field<ListGraphType<UserType>>(
              "users",
              resolve: context => userRepo.GetAllAsync()
            );
            // User By ID
            Field<UserType>
                ("user",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
                { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<string>("id");
                    try
                    {
                        return userRepo.GetUserById(id);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            );
        }

    }
}
